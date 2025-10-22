using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtisanMarketplace.Models
{
    /// <summary>
    /// Reporting system for flagging inappropriate content
    /// </summary>
    [Table("Reports")]
    [Index(nameof(Status), nameof(CreatedAt))]
    public class Report
    {
        public Report()
        {
            Id = Guid.NewGuid();
            Status = ReportStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        // The user who submitted the report
        [Required]
        public Guid ReporterId { get; set; }

        [ForeignKey(nameof(ReporterId))]
        public virtual AppUser Reporter { get; set; } = null!;

        // === Content being reported ===
        [Required]
        [StringLength(20)]
        public ContentType ContentType { get; set; }

        public Guid? UserFeedId { get; set; }
        [ForeignKey(nameof(UserFeedId))]
        public virtual UserFeed? UserFeed { get; set; }

        public Guid? ArtisanFeedId { get; set; }
        [ForeignKey(nameof(ArtisanFeedId))]
        public virtual ArtisanFeed? ArtisanFeed { get; set; }

        public Guid? CommentId { get; set; }
        [ForeignKey(nameof(CommentId))]
        public virtual Comment? Comment { get; set; }

        public Guid? ReportedUserId { get; set; }
        [ForeignKey(nameof(ReportedUserId))]
        public virtual AppUser ReportedUser { get; set; }

        // === Report Details ===
        [Required]
        [StringLength(20)]
        public ReportReason Reason { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public ReportStatus Status { get; set; } = ReportStatus.Pending;

        // === Resolution ===
        public Guid? ReviewedById { get; set; }

        [ForeignKey(nameof(ReviewedById))]
        public virtual AppUser ReviewedBy { get; set; }

        public string? ResolutionNotes { get; set; }

        // === Timestamps ===
        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public override string ToString()
        {
            return $"Report by {Reporter?.FullName ?? "Unknown"} - {Reason}";
        }
    }

    // ================= ENUMS =================

    public enum ReportReason
    {
        Spam,
        Inappropriate,
        Scam,
        Misleading,
        Harassment,
        Copyright,
        Other
    }

    public enum ReportContentType
{
    UserFeed,
    ArtisanFeed,
    Comment,
    User
}

    public enum ReportStatus
    {
        Pending,
        UnderReview,
        Resolved,
        Dismissed
    }
}
