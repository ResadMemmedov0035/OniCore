using Microsoft.AspNetCore.Builder;

namespace OniCore.CrossCuttingConcerns.ExceptionHandling.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
