using Microsoft.Extensions.DependencyInjection;
using OniCore.Security.Tokens;

namespace OniCore.Security
{
    public static class SecurityServiceRegistration
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, JwtManager>();
            // email auth
            // otp auth
            return services;
        }
    }
}
