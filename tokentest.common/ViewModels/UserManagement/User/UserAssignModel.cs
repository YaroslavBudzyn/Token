using System.ComponentModel.DataAnnotations;

namespace tokentest.Common.ViewModels.UserManagement.User
{
    public class UserAssignModel
    {
        [Required(ErrorMessage = "User Id is required.")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role Id is required.")]
        public int RoleId { get; set; }
    }
}
