using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Auth;

public class AuthHelper
{
    private readonly IConfiguration _configuration;
    private readonly int _iterations = 100000;

    public AuthHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public byte[] GetPasswordHash(string password, byte[] salt)
    {
        var pepperString = _configuration.GetSection("AuthorizationSettings:Pepper").Value!;
        var pepper = Encoding.UTF8.GetBytes(pepperString);
        var combinedPepperSalt = new byte[salt.Length + pepper.Length];
        Buffer.BlockCopy(pepper, 0, combinedPepperSalt, 0, pepper.Length);
        Buffer.BlockCopy(salt, 0, combinedPepperSalt, pepper.Length, salt.Length);

        var passwordHash =
            KeyDerivation.Pbkdf2(password, combinedPepperSalt, KeyDerivationPrf.HMACSHA512, _iterations, 64);

        return passwordHash;
    }

    public string CreateJwtToken(int userId)
    {
        var claims = new[]
        {
            new Claim("id", userId.ToString())
        };

        var tokenByteArray = Encoding.UTF8.GetBytes(_configuration.GetSection("AuthorizationSettings:TokenKey").Value!);
        var tokenSecurityKey = new SymmetricSecurityKey(tokenByteArray);

        var signingCredentials = new SigningCredentials(tokenSecurityKey, SecurityAlgorithms.HmacSha512Signature);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(0.5),
            SigningCredentials = signingCredentials
        };

        var handler = new JwtSecurityTokenHandler();

        var token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }

    public string GenerateRefreshToken(int size = 64)
    {
        var randomBytes = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }
}
