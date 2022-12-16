using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.ViewModels.Account;

namespace CleanArchitecture.Application.Entities.UserCommands.Create;

public class RegisterUserCommand : IBaseCommand<RegisterUserResult>
{
    public RegisterUserViewModel RegisterUserViewModel { get; set; }

    public RegisterUserCommand(RegisterUserViewModel registerUserViewModel)
    {
        RegisterUserViewModel = registerUserViewModel;
    }
}

