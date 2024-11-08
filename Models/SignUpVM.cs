using System.ComponentModel.DataAnnotations;

namespace DoAn_API.Models
{
    public class SignUpVM
    {
        [Required]
        public string fullName { get; set; } = null!;
        [Required, EmailAddress]

        public string email { get; set; } = null!;
        [Required]

        public string password { get; set; } = null!;
        [Required]
        public string confirmPassword { get; set; } = null!;
        public string image { get; set; } = null!;
        public DateTime dob { get; set; }
        public string gender { get; set; } = null!;
        public string address { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;

    }
}
