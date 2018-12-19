using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tokentest.Common.ViewModels.UserManagement.User
{
    public class UserSearchModel
    {
        [DefaultValue(0)]
        public int? Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? RoleId { get; set; }

        public string Role { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
