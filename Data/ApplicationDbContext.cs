using ArtisanMarketplace.Models;
using ArtisanMarketplace.Models.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Artisan_Project.Data

{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ------------------- DbSets -------------------
        public DbSet<ArtisanFeed> ArtisanFeeds { get; set; }
        public DbSet<ArtisanWork> ArtisanWorks { get; set; }
        public DbSet<BaseRole> BaseRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<UserFeed> UserFeeds { get; set; }
        public DbSet<ArtisanProfile> ArtisanProfiles { get; set; }

        // ------------------- Model Configuration -------------------
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Identity configuration must come first

            // ------------------- BaseRole configuration -------------------
            builder.Entity<BaseRole>()
                .HasIndex(r => new { r.UserId, r.RoleType })
                .IsUnique();

            // ------------------- Comment configuration -------------------
            builder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasIndex(c => new { c.CommentType, c.CreatedAt })
                      .HasDatabaseName("IX_Comments_CommentType_CreatedAt");

                entity.HasOne(c => c.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.UserFeed)
                      .WithMany(uf => uf.Comments)
                      .HasForeignKey(c => c.UserFeedId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.ArtisanFeed)
                      .WithMany(af => af.Comments)
                      .HasForeignKey(c => c.ArtisanFeedId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.ParentComment)
                      .WithMany(c => c.Replies)
                      .HasForeignKey(c => c.ParentCommentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(c => c.CommentType)
                      .HasConversion<string>();
            });

            // ------------------- Reaction configuration -------------------
            builder.Entity<Reaction>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.HasIndex(r => new { r.UserId, r.ContentType, r.UserFeedId, r.ArtisanFeedId, r.CommentId })
                      .IsUnique()
                      .HasDatabaseName("IX_Reactions_Unique");

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reactions)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.UserFeed)
                      .WithMany(uf => uf.Reactions)
                      .HasForeignKey(r => r.UserFeedId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.ArtisanFeed)
                      .WithMany(af => af.Reactions)
                      .HasForeignKey(r => r.ArtisanFeedId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Comment)
                      .WithMany(c => c.Reactions)
                      .HasForeignKey(r => r.CommentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(r => r.ReactionType)
                      .HasConversion<string>();

                entity.Property(r => r.ContentType)
                      .HasConversion<string>();
            });

            // ------------------- Report configuration -------------------
            builder.Entity<Report>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.HasIndex(r => new { r.Status, r.CreatedAt })
                      .HasDatabaseName("IX_Reports_Status_CreatedAt");

                entity.HasOne(r => r.Reporter)
                      .WithMany()
                      .HasForeignKey(r => r.ReporterId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.UserFeed)
                      .WithMany(uf => uf.Reports)
                      .HasForeignKey(r => r.UserFeedId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.ArtisanFeed)
                      .WithMany(af => af.Reports)
                      .HasForeignKey(r => r.ArtisanFeedId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Comment)
                      .WithMany(c => c.Reports)
                      .HasForeignKey(r => r.CommentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.ReportedUser)
                      .WithMany()
                      .HasForeignKey(r => r.ReportedUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.ReviewedBy)
                      .WithMany()
                      .HasForeignKey(r => r.ReviewedById)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(r => r.Reason)
                      .HasConversion<string>();

                entity.Property(r => r.ContentType)
                      .HasConversion<string>();

                entity.Property(r => r.Status)
                      .HasConversion<string>();
            });
        }
    }
}
