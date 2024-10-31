using Microsoft.AspNetCore.Identity;

namespace DoAn_API.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

    }
}
