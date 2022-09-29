using Microsoft.Extensions.DependencyInjection;
using OniCore.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
