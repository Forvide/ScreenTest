using Company.Delivery.Api.Exception;
using Company.Delivery.Domain;
using System.Net;
using System.Text.Json;

namespace Company.Delivery.Api;
/// <summary>
/// ExceptionMiddleware
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    /// <param name="env"></param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    /// <summary>
    /// InvokeAsync
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception e)
        {
            context.Response.ContentType = "application/json";
            var jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            if (e is EntityNotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(string.Empty);
                return;
            }
            _logger.LogError(e, "By ExceptionMiddleware:");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment()
                ? new ExceptionModel { StatusCode = context.Response.StatusCode, Message = e.Message, Content = e.StackTrace }
                : new ExceptionModel { StatusCode = context.Response.StatusCode, Message = e.Message, Content = "Internal Server Error" };


            var json = JsonSerializer.Serialize(response, jsonSerializerOptions);

            await context.Response.WriteAsync(json);
        }
    }
}