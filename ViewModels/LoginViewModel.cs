using System.ComponentModel.DataAnnotations;

namespace Artisan_Project.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string EmailOrPhone { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
