using System.ComponentModel.DataAnnotations;

namespace Artisan_Project.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Phone]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        // Address info
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }

        // Artisan-only fields (matching controller)
        public bool IsArtisan { get; set; }
        public string? Bio { get; set; }                 // used in controller
        public string? Address { get; set; }             // used in controller
        public string? ArtisanSpeciality { get; set; }   // used in controller
        public string? ProfessionalBio { get; set; }    // optional, keep if controller uses it
        public string? BusinessAddress { get; set; }    // optional, keep if controller uses it
    }
}
