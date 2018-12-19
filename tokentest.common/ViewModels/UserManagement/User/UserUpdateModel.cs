using System.ComponentModel.DataAnnotations;

namespace tokentest.Common.ViewModels.UserManagement.User
{
    public class UserUpdateModel
    {
        [Required(ErrorMessage = "ID is required.")]
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
