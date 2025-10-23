using System;
using System.Collections.Generic;

namespace ArtisanMarketplace.Models.Roles
{
    /// <summary>
    /// Factory for creating role instances dynamically based on role type.
    /// </summary>
    public static class RoleFactory
    {
        public static BaseRole CreateRole(string roleType)
        {
            roleType = roleType?.Trim().ToUpper() ?? string.Empty;

            return roleType switch
            {
                RoleTypes.User => new UserRole(),
                RoleTypes.Artisan => new ArtisanRole(),
                RoleTypes.Admin => new AdminRole(),
                RoleTypes.Moderator => new ModeratorRole(),

                // ✅ Handles all artisan-related specializations dynamically
                _ when RoleTypes.ArtisanAliases.Contains(roleType) => CreateArtisanWithSpecialization(roleType),

                _ => throw new ArgumentException($"Invalid role type: {roleType}")
            };
        }

        public static BaseRole CreateRoleForUser(Guid userId, string roleType)
        {
            var role = CreateRole(roleType);
            role.UserId = userId;
            return role;
        }

        /// <summary>
        /// Dynamically create an ArtisanRole for any specialization (e.g., Mechanic, Welder, Mason, etc.)
        /// </summary>
        private static ArtisanRole CreateArtisanWithSpecialization(string specialization)
        {
            var artisan = new ArtisanRole();
            artisan.AddSpecialization(specialization);
            return artisan;
        }

        /// <summary>
        /// Returns a dictionary of all possible roles for dropdown display.
        /// </summary>
        public static Dictionary<string, string> GetAllRoleDisplayNames()
        {
            return new Dictionary<string, string>
            {
                { RoleTypes.User, "Regular User" },
                { RoleTypes.Artisan, "Artisan (General)" },
                { RoleTypes.Admin, "Administrator" },
                { RoleTypes.Moderator, "Moderator" }
            };
        }
    }
}
