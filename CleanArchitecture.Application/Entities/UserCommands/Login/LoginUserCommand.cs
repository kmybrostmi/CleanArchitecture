using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.ViewModels.User;

namespace CleanArchitecture.Application.Entities.UserCommands.Login;
public class LoginUserCommand : IBaseCommand<LoginUserResult>
{
    public LoginUserViewModel LoginUserViewModel { get; set; }

    public LoginUserCommand(LoginUserViewModel loginUserViewModel)
    {
        LoginUserViewModel = loginUserViewModel;
    }
}
