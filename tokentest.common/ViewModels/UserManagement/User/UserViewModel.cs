using System.ComponentModel.DataAnnotations;

namespace tokentest.Common.ViewModels.UserManagement.User
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public int RoleId { get; set; }

        public string Role { get; set; }
    }
}
