using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanMarketplace.Models
{
    [Table("Reactions")]
    public class Reaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; } = null!; // nullable to avoid init warnings

        [Required]
        public ReactionType ReactionType { get; set; }

        [Required]
        public ContentType ContentType { get; set; }

        public Guid? UserFeedId { get; set; }

        [ForeignKey(nameof(UserFeedId))]
        public virtual UserFeed? UserFeed { get; set; } = null!;

        public Guid? ArtisanFeedId { get; set; }

        [ForeignKey(nameof(ArtisanFeedId))]
        public virtual ArtisanFeed? ArtisanFeed { get; set; } = null!;

        public Guid? CommentId { get; set; }

        [ForeignKey(nameof(CommentId))]
        public virtual Comment? Comment { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum ReactionType
    {
        [Display(Name = "Like")]
        Like,

        [Display(Name = "Dislike")]
        Dislike
    }

    public enum ContentType
    {
        [Display(Name = "User Feed")]
        UserFeed,

        [Display(Name = "Artisan Feed")]
        ArtisanFeed,

        [Display(Name = "Comment")]
        Comment
    }
}
