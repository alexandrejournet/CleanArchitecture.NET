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
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error",
                Source = exception.Source,
                StackTrace = exception.StackTrace
            };


            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}
