using DoAn_API.Models;
using Microsoft.AspNetCore.Identity;

namespace DoAn_API.Services
{
    public interface IAccountRepository
    {

        public Task<IdentityResult> SignUpAsync(SignUpVM model);
        public Task<string> SignInAsync(SignInVM model);

    }
}
