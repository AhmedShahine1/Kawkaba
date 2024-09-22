using System.Net;
using System.Text.Json;
using InitialProject.Core.DTO;


namespace InitialProject.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _env = env;
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // var endpoint = context.GetEndpoint(); // endpoint != null && 
        bool isApi = context.Request.Path.StartsWithSegments("/api");
        if (isApi)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new BaseResponse()
                    {
                        ErrorCode = (int)HttpStatusCode.InternalServerError,
                        ErrorMessage = ex.Message,
                        Data = ex.StackTrace
                    }
                    : new BaseResponse()
                    {
                        ErrorCode = (int)HttpStatusCode.InternalServerError,
                        ErrorMessage = "Internal Server Error"
                    };


                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
        else
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_env.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message);
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var response = _env.IsDevelopment()
                        ? new BaseResponse()
                        {
                            ErrorCode = (int)HttpStatusCode.InternalServerError,
                            ErrorMessage = ex.Message,
                            Data = ex.StackTrace
                        }
                        : new BaseResponse()
                        {
                            ErrorCode = (int)HttpStatusCode.InternalServerError,
                            ErrorMessage = "Internal Server Error"
                        };


                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                    var json = JsonSerializer.Serialize(response, options);

                    await context.Response.WriteAsync(json);
                }
                else
                {
                    context.Response.Redirect("/ErrorsMvc/Index?code=500");
                }

            }
        }
    }
}
