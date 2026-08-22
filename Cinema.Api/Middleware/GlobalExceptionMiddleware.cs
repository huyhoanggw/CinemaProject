using SeedWorks.Reponse;
using System.Text.Json;

namespace Cinema.Api.Middleware
{
    public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception. TraceId: {TraceId}",
                 context.TraceIdentifier);
                await HandlerExceptionAsync(context, ex);
            }

        }
        public static async Task HandlerExceptionAsync(HttpContext context, Exception ex) 
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound ,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            // bằng với đoạn này 
            //if (ex is KeyNotFoundException)
            //{
            //    context.Response.StatusCode = StatusCodes.Status404NotFound;
            //}
            //else if (ex is UnauthorizedAccessException)
            //{
            //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //}
            //else if (ex is ArgumentException)
            //{
            //    context.Response.StatusCode = StatusCodes.Status400BadRequest;
            //}
            //else
            //{
            //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //}
            var reponse = new ApiErrorResult<bool>(ex.Message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(reponse));
        }
    }
}
