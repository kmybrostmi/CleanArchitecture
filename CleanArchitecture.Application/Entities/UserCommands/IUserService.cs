using CleanArchitecture.Application.Entities.UserCommands.Login;
using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Account;

namespace CleanArchitecture.Application.Entities.UserCommands;

public interface IUserService
{
    Task<Users> GetUserByPhoneNumber(string phoneNumber);
    Task<LoginUserResult> LoginUser(LoginUserViewModel loginUser);
    Task<RegisterUserResult> RegisterUser(RegisterUserViewModel loginUser);
    Task<ActiveAccountResult> ActiveAccount(ActiveAccountViewModel loginUser);
    Task<Users> GetUserById(Guid id);
}

