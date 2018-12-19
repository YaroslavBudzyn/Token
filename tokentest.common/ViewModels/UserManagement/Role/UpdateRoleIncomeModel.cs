using System.ComponentModel.DataAnnotations;

namespace tokentest.Common.ViewModels.UserManagement.Role
{
    public class UpdateRoleIncomeModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
