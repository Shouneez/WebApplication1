using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtoos
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string UserType { get; set; }
    }
}
