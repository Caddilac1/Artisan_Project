using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArtisanMarketplace.Data;
using ArtisanMarketplace.Models;
using ArtisanMarketplace.Models.Roles;

namespace ArtisanMarketplace.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BaseRole>> GetUserRolesAsync(Guid userId)
        {
            var roleEntities = await _context.Roles
                .Where(r => r.UserId == userId && r.IsActive)
                .ToListAsync();

            return roleEntities.Select(r => RoleFactory.CreateRole(r.RoleType)).ToList();
        }

        public async Task<bool> UserHasPermissionAsync(Guid userId, string permission)
        {
            var roles = await GetUserRolesAsync(userId);
            return roles.Any(r => r.HasPermission(permission));
        }

        public async Task<bool> UserHasRoleAsync(Guid userId, string roleType)
        {
            return await _context.Roles
                .AnyAsync(r => r.UserId == userId
                               && r.RoleType.ToUpper() == roleType.ToUpper()
                               && r.IsActive);
        }

        public async Task<BaseRole> AssignRoleAsync(Guid userId, string roleType, bool isPrimary = false)
        {
            var existingRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.UserId == userId && r.RoleType.ToUpper() == roleType.ToUpper());

            if (existingRole != null)
            {
                existingRole.IsActive = true;
                existingRole.IsPrimary = isPrimary;
                await _context.SaveChangesAsync();
                return RoleFactory.CreateRole(roleType);
            }

            var baseRole = RoleFactory.CreateRoleForUser(userId, roleType);
            baseRole.IsPrimary = isPrimary;

            if (isPrimary)
            {
                var existingPrimaryRoles = await _context.Roles
                    .Where(r => r.UserId == userId && r.IsPrimary)
                    .ToListAsync();

                foreach (var role in existingPrimaryRoles)
                    role.IsPrimary = false;
            }

            var roleEntity = new Role
            {
                UserId = baseRole.UserId,
                RoleType = baseRole.RoleType,
                IsActive = baseRole.IsActive,
                IsPrimary = baseRole.IsPrimary,
                AssignedDate = baseRole.AssignedDate
            };

            _context.Roles.Add(roleEntity);
            await _context.SaveChangesAsync();

            return baseRole;
        }

        public async Task<bool> RemoveRoleAsync(Guid userId, string roleType)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.UserId == userId && r.RoleType.ToUpper() == roleType.ToUpper());

            if (role == null)
                return false;

            role.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BaseRole?> GetPrimaryRoleAsync(Guid userId)
        {
            var primaryRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.UserId == userId && r.IsPrimary && r.IsActive);

            return primaryRole == null ? null : RoleFactory.CreateRole(primaryRole.RoleType);
        }

        public async Task<List<string>> GetUserPermissionsAsync(Guid userId)
        {
            var roles = await GetUserRolesAsync(userId);
            var permissions = new HashSet<string>();

            foreach (var role in roles)
                foreach (var permission in role.GetPermissions())
                    permissions.Add(permission);

            return permissions.ToList();
        }
    }
}
