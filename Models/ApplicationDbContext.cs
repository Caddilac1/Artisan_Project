using ArtisanMarketplace.Models;
using ArtisanMarketplace.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace ArtisanMarketplace.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ✅ Only include models that represent data tables
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<ArtisanFeed> ArtisanFeeds { get; set; }
        public DbSet<ArtisanWork> ArtisanWorks { get; set; }
        public DbSet<BaseRole> BaseRoles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<ArtisanProfile> ArtisanProfiles { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserFeed> UserFeeds { get; set; }
        public DbSet<User> Users { get; set; }

        // ... Add other actual data models if needed
    }
}
