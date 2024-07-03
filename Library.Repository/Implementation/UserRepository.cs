using System.Security.Claims;
using Library.Repository.Contract;
using Library.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Repository.Implementation
{
	public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<SignInResult> LoginUserAsync(UserLoginModel userLoginModel)
        {
            var user = await userManager.FindByEmailAsync(userLoginModel.Email);
            if (user == null)
            {
                throw new Exception("User not registered with the system");
            }

            var isValidCredentials = await userManager.CheckPasswordAsync(user, userLoginModel.Password);
            if (!isValidCredentials)
            {
                throw new Exception("Invalid credentials");
            }

            var result = await signInManager.PasswordSignInAsync(userLoginModel.Email, userLoginModel.Password,
                userLoginModel.RememberMe, true);
            if (result.Succeeded)
            {
                var userLogin = new UserLoginInfo("Web", "Cookie", "Web");
                await userManager.AddLoginAsync(user, userLogin);
            }
            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegistrationModel userRegistrationModel, bool isAdminCreated = true)
        {
            var user = await userManager.FindByEmailAsync(userRegistrationModel.Email);
            if (user != null)
            {
                throw new Exception("Email already exists.");
            }

            var newUser = new IdentityUser
            {
                UserName = userRegistrationModel.Email,
                NormalizedUserName = userRegistrationModel.Email,
                Email = userRegistrationModel.Email,
                PhoneNumber = userRegistrationModel.PhoneNumber,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false
            };
            var result = await userManager.CreateAsync(newUser, userRegistrationModel.Password);
            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userRegistrationModel.Name),
                    new Claim(ClaimTypes.Email, userRegistrationModel.Email),
                    new Claim(ClaimTypes.MobilePhone, userRegistrationModel.PhoneNumber),
                    new Claim(ClaimTypes.Role, userRegistrationModel.IsAdmin ? "Admin" : "User")
                };
                await this.userManager.AddClaimsAsync(newUser, claims);
            }
            return result;
        }

        public async Task SignOutAsync()
        {
            await this.signInManager.SignOutAsync();
        }
    }
}