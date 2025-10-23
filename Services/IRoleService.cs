namespace ArtisanMarketplace.Services
{
    public interface IRoleService
    {
        Task<bool> RoleExistsAsync(string roleName);
        Task CreateRoleAsync(string roleName);
        Task AssignRoleToUserAsync(Guid userId, string roleName);

        // Add these two methods
        Task<bool> UserHasRoleAsync(Guid userId, string roleName);
        Task<bool> UserHasPermissionAsync(Guid userId, string permission);
    }
}
