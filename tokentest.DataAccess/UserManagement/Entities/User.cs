using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace tokentest.DataAccess.UserManagement.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [DefaultValue(true)]
        [StringLength(255)]
        public string ResetPasswordToken { get; set; }
    }
}
