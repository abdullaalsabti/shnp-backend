using System.Diagnostics;

namespace WebApplication1.Middleware;

public class RequestTimingMiddleware
{
    private readonly ILogger<RequestTimingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await _next(context);
        stopwatch.Stop();

        _logger.LogInformation($"Request {context.Request.Path} took {stopwatch.ElapsedMilliseconds} milliseconds");
    }
}
