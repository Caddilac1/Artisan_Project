// ArtisanMarketplace/Models/Roles/Permissions.cs
namespace ArtisanMarketplace.Models.Roles
{
    public static class Permissions
    {
        public static readonly List<string> BasicUserPermissions = new()
        {
            "ViewProfile",
            "EditProfile",
            "BrowseMarketplace"
        };

        public static readonly List<string> AdminPermissions = new()
        {
            "ManageUsers",
            "ApproveListings",
            "BanUsers"
        };

        public static readonly List<string> ArtisanPermissions = new()
        {
            "CreateListing",
            "EditListing",
            "ViewOrders"
        };
    }
}
