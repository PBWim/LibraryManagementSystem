using Library.Repository.Contract;
using Library.Service.Contract;
using Library.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Service.Implementation;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<SignInResult> LoginUserAsync(UserLoginModel userLoginModel)
    {
        var result = await this.userRepository.LoginUserAsync(userLoginModel);
        return result;
    }

    public async Task<IdentityResult> CreateUserAsync(UserRegistrationModel userRegisterModel)
    {
        var result = await this.userRepository.CreateUserAsync(userRegisterModel);
        return result;
    }

    public async Task SignOutAsync()
    {
        await this.userRepository.SignOutAsync();
    }
}