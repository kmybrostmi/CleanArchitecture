using Azure.Core;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Account;
using CleanArchitecture.Infrastructure.Repositories.Entities.User;

namespace CleanArchitecture.Application.Entities.UserCommands;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHelper _passwordHelper;

    public UserService(IUserRepository repository, IPasswordHelper passwordHelper)
    {
        _repository = repository;
        _passwordHelper = passwordHelper;
    }
    public async Task<Users> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _repository.GetUserByPhoneNumber(phoneNumber);
    }

    public async Task<LoginUserResult> LoginUser(LoginUserViewModel loginUser)
    {
        if (!string.IsNullOrWhiteSpace(loginUser.PhoneNumber))
        {
            var user = await _repository.GetUserByPhoneNumber(loginUser.PhoneNumber);
            if (user == null) return LoginUserResult.Success;
            if (user.IsBlocked) return LoginUserResult.IsBlocked;
            if (user.IsDeleted) return LoginUserResult.NotFound;
            if (!user.IsMobileActive) return LoginUserResult.NotActive;
            if (user.Password != _passwordHelper.EncodePasswordMd5(loginUser.Password)) return LoginUserResult.NotFound;

            return LoginUserResult.Success;
        }
        return LoginUserResult.NotFound;
    }
}
