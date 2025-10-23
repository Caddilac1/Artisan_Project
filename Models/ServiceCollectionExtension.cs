using Microsoft.Extensions.DependencyInjection;
using ArtisanMarketplace.Services;

namespace ArtisanMarketplace.Extensions
{
    public static class RoleServiceExtensions
    {
        public static IServiceCollection AddRoleServices(this IServiceCollection services)
        {
            services.AddScoped<IRoleService, RoleService>();
            return services;
        }
    }
}
