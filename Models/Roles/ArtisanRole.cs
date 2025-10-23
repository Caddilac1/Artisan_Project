using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtisanMarketplace.Models.Roles
{
    /// <summary>
    /// Represents a general Artisan role.
    /// All artisans fall under this role but can have multiple specializations (e.g. Electrician, Plumber).
    /// </summary>
    public class ArtisanRole : BaseRole
    {
        // Example: ["Electrician", "Plumber", "Painter"]
        public List<string> Specializations { get; set; } = new();

        public ArtisanRole()
        {
            RoleType = RoleTypes.Artisan;
            IsPrimary = true;
            IsActive = true;
        }

        public override string GetRoleDisplayName() => "Artisan";

        public override List<string> GetPermissions()
        {
            // Permissions common to all artisans
            return new List<string>
            {
                Permissions.CreateListing,
                Permissions.EditListing,
                Permissions.ViewOrders,
                Permissions.ViewProfile,
                Permissions.EditProfile,
                Permissions.CreateBooking,
                Permissions.ViewBookingRequests,
                Permissions.AcceptBooking
            };
        }

        public override int GetPriorityLevel() => 50;

        // === Helper methods for managing specializations ===

        public void AddSpecialization(string specialization)
        {
            if (!string.IsNullOrWhiteSpace(specialization) &&
                !Specializations.Contains(specialization, StringComparer.OrdinalIgnoreCase))
            {
                Specializations.Add(specialization);
            }
        }

        public void RemoveSpecialization(string specialization)
        {
            Specializations.RemoveAll(s => s.Equals(specialization, StringComparison.OrdinalIgnoreCase));
        }

        public string GetSpecializationsSummary()
        {
            return Specializations.Any()
                ? string.Join(", ", Specializations)
                : "No specializations listed";
        }
    }
}
