using Library.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Repository.Contract
{
	public interface IUserRepository
	{
        Task<SignInResult> LoginUserAsync(UserLoginModel userLoginModel);

        Task<IdentityResult> CreateUserAsync(UserRegistrationModel userRegistrationModel, bool isAdminCreated = false);

		Task SignOutAsync();
	}
}