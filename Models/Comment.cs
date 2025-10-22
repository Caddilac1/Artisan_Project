using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanMarketplace.Models
{
    public partial class Comment
    {
        [Key]
        public Guid Id { get; set; }

        // User who made the comment
        [Required]
        public Guid UserId { get; set; }

        // Optional relationships
        public Guid? UserFeedId { get; set; }
        public Guid? ArtisanFeedId { get; set; }
        public Guid? ParentCommentId { get; set; }

        // Type of comment (enum or string)
        [Required]
        public string CommentType { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; } = null!;

        [ForeignKey(nameof(UserFeedId))]
        public virtual UserFeed? UserFeed { get; set; }

        [ForeignKey(nameof(ArtisanFeedId))]
        public virtual ArtisanFeed? ArtisanFeed { get; set; }

        [ForeignKey(nameof(ParentCommentId))]
        public virtual Comment? ParentComment { get; set; }

        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
        public virtual ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
