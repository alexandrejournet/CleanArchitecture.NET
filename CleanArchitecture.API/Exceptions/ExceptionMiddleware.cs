using CleanArchitecture.Domain.Exceptions;
using System.Text.Json;

namespace CleanArchitecture.API.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ProjectException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, ProjectException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = exception.StatusCode ?? StatusCodes.Status500InternalServerError;
            var responseModel = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message ?? "Internal Server Error",
                Source = exception.Source
            };


            string result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await response.WriteAsync(result);
        }
    }
}
