using CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace CleanArchitecture.Application.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
