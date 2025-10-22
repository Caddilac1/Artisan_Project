using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtisanMarketplace.Models;


namespace Artisan_Project.Models
{
    public class ArtisanProposal
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Relationships
        [Required]
        public Guid UserFeedId { get; set; }

        [ForeignKey("UserFeedId")]
        public virtual UserFeed? UserFeed { get; set; }

        [Required]
        public Guid ArtisanId { get; set; }

        [ForeignKey("ArtisanId")]
        public virtual ArtisanProfile? Artisan { get; set; }

        // Proposal details
        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue)]
        public decimal ProposedPrice { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 day.")]
        public int EstimatedDuration { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; } = string.Empty;

        // Terms and Conditions
        [DataType(DataType.MultilineText)]
        public string? TermsConditions { get; set; }

        [DataType(DataType.MultilineText)]
        public string? PaymentTerms { get; set; }

        // File Upload (Quote document)
        [MaxLength(255)]
        public string? QuoteDocument { get; set; }  // store the file path

        // Status
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "PENDING";

        // Timestamps
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? AcceptedAt { get; set; }

        // Optional: helper method to update timestamps
        public void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
