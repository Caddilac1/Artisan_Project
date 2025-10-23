using Microsoft.Extensions.DependencyInjection;
using ArtisanMarketplace.Services;


namespace ArtisanMarketplace.Extensions
{
    /// <summary>
    /// Extension methods for configuring role services
    /// </summary>
    public static class RoleServiceExtensions
    {
        /// <summary>
        /// Add role management services to the dependency injection container
        /// </summary>
        public static IServiceCollection AddRoleServices(this IServiceCollection services)
        {
            services.AddScoped<IRoleService, RoleService>();
            
            return services;
        }
    }
}
