namespace ArtisanMarketplace.Models.Roles
{
    public static class RoleTypes
    {
        public const string User = "USER";
        public const string Artisan = "ARTISAN";
        public const string Admin = "ADMIN";
        public const string Moderator = "MODERATOR";

        // ✅ Optional: Define known artisan aliases
        public static readonly string[] ArtisanAliases = new[]
        {
            "ELECTRICIAN", "PLUMBER", "MASON", "CARPENTER",
            "PAINTER", "TILER", "ROOFER", "MECHANIC", "WELDER", "TECHNICIAN"
        };
    }
}
