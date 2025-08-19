using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto;

namespace WebApplication1.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly DataContextEf _contextEf;

    public ProfileController(IConfiguration configuration)
    {
        _contextEf = new DataContextEf(configuration);
    }

    [HttpGet("me", Name = "GetMe")]
    public async Task<ActionResult<MeDto>> GetMe()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized(new { error = "User ID Not Found in Token" });

        if (!int.TryParse(userIdClaim, out var userId)) return Unauthorized(new { error = "User ID Invalid in Token" });

        var user = await _contextEf.Users.Include(u => u.Documents)
            .ThenInclude(d => d.Urls)
            .Include(u => u.WorkingDetails)
            .Include(u => u.RestaurantTypes)
            .SingleOrDefaultAsync(u => u.UserId == userId);

        // FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null) return NotFound(new { error = "User Not Found in Token" });

        return Ok(new MeDto(user));
    }
}
