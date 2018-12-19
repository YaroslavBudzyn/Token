using System.ComponentModel.DataAnnotations;

namespace tokentest.Common.ViewModels.UserManagement.User
{
    public class LoginIncomeModel
    {
        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
