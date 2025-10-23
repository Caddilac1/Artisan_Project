using System.Collections.Generic;

namespace ArtisanMarketplace.Models.Roles
{
    public static class Permissions
    {
        // ===================== BASIC USER PERMISSIONS =====================
        public static readonly List<string> BasicUserPermissions = new()
        {
            "ViewProfile",
            "EditProfile",
            "BrowseMarketplace",
            "CreateBooking",
            "ViewBookingRequests",
            "AcceptBooking"
        };

        // ===================== ARTISAN PERMISSIONS =====================
        public static readonly List<string> ArtisanPermissions = new()
        {
            "CreateListing",
            "EditListing",
            "ViewOrders",
            "AcceptBooking",
            "ViewBookingRequests"
        };

        // ===================== ADMIN PERMISSIONS =====================
        public static readonly List<string> AdminPermissions = new()
        {
            "ManageUsers",
            "ApproveListings",
            "BanUsers",
            "ViewOrders",
            "CreateListing",
            "EditListing"
        };
    }
}
