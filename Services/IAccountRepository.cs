using DoAn_API.Models;
using Microsoft.AspNetCore.Identity;

namespace DoAn_API.Services
{
    public interface IAccountRepository
    {

        public Task<IdentityResult> SignUpAsync(SignupRequest request);
        public Task<string> SignInAsync(SignInVM model);
        public Task<string> UploadImage(IFormFile file);

    }
}
