using Library.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Service.Contract
{
	public interface IUserService
	{
        Task<SignInResult> LoginUserAsync(UserLoginModel userLoginModel);

        Task<IdentityResult> CreateUserAsync(UserRegistrationModel userRegisterModel);

        Task SignOutAsync();
    }
}