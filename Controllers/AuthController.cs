using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Auth;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Dto;
using WebApplication1.Validation;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthHelper _authHelper;
    private readonly DataContextEf _contextEf;

    public AuthController(IConfiguration configuration)
    {
        _contextEf = new DataContextEf(configuration);
        _authHelper = new AuthHelper(configuration);
    }

    //TODO: REGISTER
    [AllowAnonymous]
    [HttpPost("Register", Name = "Register")]
    public async Task<ActionResult> RegisterNewUser([FromBody] UserForRegistrationDto userForRegistrationDto)
    {
        var validator = new UserRegistrationValidator();
        var validationResult = await validator.ValidateAsync(userForRegistrationDto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        // Hash password
        var salt = new byte[256 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var passwordHash = _authHelper.GetPasswordHash(userForRegistrationDto.Password, salt);

        var exists = await _contextEf.UserAuth.AnyAsync(u => u.Email == userForRegistrationDto.Email);
        if (exists) return BadRequest("Email already exists");

        var authEntity = new UserAuth
        {
            Email = userForRegistrationDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = salt,
            CreatedAt = DateTime.UtcNow
        };
        _contextEf.UserAuth.Add(authEntity);

        // Create User entity
        var user = Restaurant.CreateFromRegistration(userForRegistrationDto, _contextEf);

        _contextEf.Users.Add(user);

        var affectedRows = await _contextEf.SaveChangesAsync();
        return affectedRows > 0
            ? Ok(new
            {
                message = "User created successfully.",
                email = user.Email,
                success = true
            })
            : StatusCode(500, new { message = "Failed to create user" });
    }


    //TODO: LOGIN
    [AllowAnonymous]
    [HttpPost("Login", Name = "Login")]
    public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        var user = await _contextEf.UserAuth.FirstOrDefaultAsync(u => u.Email == userForLoginDto.Email);
        if (user == null) return BadRequest("User not found");

        var salt = user.PasswordSalt;
        var createdHash = _authHelper.GetPasswordHash(userForLoginDto.Password, salt);
        var storedHash = user.PasswordHash;


        for (var i = 0; i < createdHash.Length; i++)
            if (createdHash[i] != storedHash[i])
                return Unauthorized("Invalid password");

        var userId = user.UserId;

        var jwtToken = _authHelper.CreateJwtToken(userId);
        var refreshToken = _authHelper.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(30)
        };

        _contextEf.RefreshToken.Add(refreshTokenEntity);
        var affectedRows = await _contextEf.SaveChangesAsync();

        return affectedRows > 0 ? Ok(new { jwtToken, refreshToken }) : BadRequest("Failed to create user");
    }

    //TODO: REFRESH TOKEN
    [AllowAnonymous]
    [HttpPost("RefreshToken", Name = "RefreshToken")]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshRequestDto refreshToken)
    {
        var tokenEntity =
            await _contextEf.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == refreshToken.RefreshToken);
        if (!AuthUtils.CheckStillValidRefreshToken(tokenEntity))
            return BadRequest("Invalid refresh token");

        var newJwtToken = _authHelper.CreateJwtToken(tokenEntity!.UserId);

        tokenEntity.Token = _authHelper.GenerateRefreshToken();
        tokenEntity.ExpiresAt = DateTime.UtcNow.AddDays(30);
        await _contextEf.SaveChangesAsync();

        return Ok(new
        {
            jwtToken = newJwtToken, refreshToken = tokenEntity.Token
        });
    }

    //TODO: LOGOUT
    [Authorize]
    [HttpPost("Logout", Name = "Logout")]
    public async Task<ActionResult> Logout([FromBody] LogoutRequestDto request)
    {
        var storedRefreshToken =
            await _contextEf.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);

        if (!AuthUtils.CheckStillValidRefreshToken(storedRefreshToken))
            return BadRequest("Invalid refresh token");

        storedRefreshToken!.IsRevoked = true;
        var affectedRows = await _contextEf.SaveChangesAsync();
        return affectedRows > 0 ? Ok("Logged out successfully") : StatusCode(500, "Failed to log out");
    }
}