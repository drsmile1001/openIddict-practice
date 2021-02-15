using System.ComponentModel.DataAnnotations;

namespace OpenIddictPractice.Server.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        public string Account { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}