namespace ArtisanMarketplace.Models.Roles
{
    public static class Permissions
    {
        // =====================
        // BASIC USER PERMISSIONS
        // =====================
        public const string ViewProfile = "ViewProfile";
        public const string EditProfile = "EditProfile";
        public const string BrowseMarketplace = "BrowseMarketplace";
        public const string CreateBooking = "CreateBooking";
        public const string ViewBookingRequests = "ViewBookingRequests";
        public const string AcceptBooking = "AcceptBooking";

        // =====================
        // ADMIN PERMISSIONS
        // =====================
        public const string ManageUsers = "ManageUsers";
        public const string ApproveListings = "ApproveListings";
        public const string BanUsers = "BanUsers";

        // =====================
        // ARTISAN PERMISSIONS
        // =====================
        public const string CreateListing = "CreateListing";
        public const string EditListing = "EditListing";
        public const string ViewOrders = "ViewOrders";

        // =====================
        // MODERATOR PERMISSIONS
        // =====================
        public const string ReviewReports = "ReviewReports";
        public const string ModerateComments = "ModerateComments";
        public const string SuspendUser = "SuspendUser";

        // =====================
        // PERMISSION GROUPS (Lists)
        // =====================

        // For Admin Role
        public static readonly List<string> AdminPermissions = new()
        {
            ManageUsers,
            ApproveListings,
            BanUsers,
            ReviewReports,
            ModerateComments,
            SuspendUser,
            ViewProfile,
            EditProfile,
            BrowseMarketplace,
            CreateBooking,
            ViewBookingRequests,
            AcceptBooking,
            CreateListing,
            EditListing,
            ViewOrders
        };

        // For Regular User
        public static readonly List<string> BasicUserPermissions = new()
        {
            ViewProfile,
            EditProfile,
            BrowseMarketplace,
            CreateBooking,
            ViewBookingRequests
        };

        // For Artisan Role
        public static readonly List<string> ArtisanPermissions = new()
        {
            CreateListing,
            EditListing,
            ViewOrders,
            ViewProfile,
            EditProfile,
            CreateBooking,
            ViewBookingRequests,
            AcceptBooking
        };

        // For Moderator Role
        public static readonly List<string> ModeratorPermissions = new()
        {
            ReviewReports,
            ModerateComments,
            SuspendUser,
            ViewProfile,
            EditProfile,
            BrowseMarketplace
        };
    }
}
