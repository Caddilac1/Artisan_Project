using ArtisanMarketplace.Models.Roles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtisanMarketplace.Services
{
    public interface IRoleService
    {
        Task<List<BaseRole>> GetUserRolesAsync(Guid userId);
        Task<bool> UserHasPermissionAsync(Guid userId, string permission);
        Task<bool> UserHasRoleAsync(Guid userId, string roleType);
        Task<BaseRole> AssignRoleAsync(Guid userId, string roleType, bool isPrimary = false);
        Task<bool> RemoveRoleAsync(Guid userId, string roleType);
        Task<BaseRole?> GetPrimaryRoleAsync(Guid userId);
        Task<List<string>> GetUserPermissionsAsync(Guid userId);
    }
}
