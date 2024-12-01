using Data.Repositories.Account;
using Microsoft.AspNetCore.Builder;

namespace Talabat_API.Service
{
    public static class DependencyInjection
    {
        internal static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IIdentityService, IdentityService>();

            

            return services;
        }
    }
}
