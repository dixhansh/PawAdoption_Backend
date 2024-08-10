using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PawAdoption_Backend.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly ILogger<CustomExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;
        private readonly string genericErrorMessage;

        public CustomExceptionHandlerMiddleware(ILogger<CustomExceptionHandlerMiddleware> logger, RequestDelegate next, IWebHostEnvironment env, IConfiguration configuration)
        {
            this.logger = logger;
            this.next = next;
            this.env = env;
            this.genericErrorMessage = configuration["ErrorMessages:Generic"] ?? "Something went wrong, We are looking into it.";
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.Unauthorized);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex, HttpStatusCode statusCode)
        {
            var errorId = Guid.NewGuid();
            logger.LogError(ex, "Error ID: {ErrorId}, Message: {Message}", errorId, ex.Message);

            var errorResponse = new
            {
                Id = errorId,
                ErrorMessage = env.IsDevelopment() ? ex.Message : genericErrorMessage,
                StackTrace = env.IsDevelopment() ? ex.StackTrace : null
            };

            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(errorResponse);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
