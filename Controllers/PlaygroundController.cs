using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApplication1.Data;
using WebApplication1.Data.Entities.Random;
using WebApplication1.Enums;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PlaygroundController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private readonly DataContextEf _context;
    private readonly ILogger<PlaygroundController> _logger;

    public PlaygroundController(IConfiguration configuration, IMemoryCache cache, ILogger<PlaygroundController> logger)
    {
        _context = new DataContextEf(configuration);
        _cache = cache;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("TopClubs", Name = "GetTopClubs")]
    public async Task<ActionResult<List<Club>>> GetTopRestaurants()
    {
        var topClubs = await _cache.GetOrCreateAsync(CacheKeysEnum.Clubs, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromSeconds(10);
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            entry.Priority = CacheItemPriority.High;
            entry.Size = 1;

            _logger.LogInformation("cache miss - fetching from DB");
            return await _context.Clubs.ToListAsync();
        });

        var stats = _cache.GetCurrentStatistics();
        _logger.LogInformation($"Cache Stats - " +
                               $"Estimated Size: {stats?.CurrentEstimatedSize ?? 0}, " +
                               $"Total Hits: {stats?.TotalHits ?? 0}, " +
                               $"Total Misses: {stats?.TotalMisses ?? 0}");

        return Ok(topClubs);
    }
}
