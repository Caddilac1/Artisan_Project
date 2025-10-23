using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArtisanMarketplace.Models.Roles
{
    public abstract class BaseRole
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }   // Add this if it’s not already there
        public string RoleType { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsPrimary { get; set; } = false;

        // ✅ NEW: when the role was assigned
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

        public abstract string GetRoleDisplayName();
        public abstract List<string> GetPermissions();
        public abstract int GetPriorityLevel();

        public bool HasPermission(string permission)
        {
            return GetPermissions().Contains(permission);
        }
    }
}
