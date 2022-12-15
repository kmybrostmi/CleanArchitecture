using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.ViewModels.User;

namespace CleanArchitecture.Application.Entities.UserCommands.Create;

public class RegisterUserCommand : IBaseCommand<RegisterUserResult>
{
    public RegisterUserViewModel RegisterUserViewModel { get; set; }

    public RegisterUserCommand(RegisterUserViewModel registerUserViewModel)
    {
        RegisterUserViewModel = registerUserViewModel;
    }
}

