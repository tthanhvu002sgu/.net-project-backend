using System.ComponentModel.DataAnnotations;

namespace DoAn_API.Models
{
    public class SignInVM
    {
        [Required, EmailAddress]
        public string email { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
    }
}
