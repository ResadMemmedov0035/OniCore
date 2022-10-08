using Microsoft.AspNetCore.Builder;

namespace OniCore.CrossCuttingConcerns.Exceptions
{
    public static class ExceptionExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
