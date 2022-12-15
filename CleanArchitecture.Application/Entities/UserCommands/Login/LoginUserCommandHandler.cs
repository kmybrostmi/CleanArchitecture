using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.ViewModels.User;
using CleanArchitecture.Infrastructure.Repositories.Entities.User;

namespace CleanArchitecture.Application.Entities.UserCommands.Login;

public class LoginUserCommandHandler : IBaseCommandHandler<LoginUserCommand, LoginUserResult>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHelper _passwordHelper;

    public LoginUserCommandHandler(IUserRepository repository,IPasswordHelper passwordHelper)
    {
        _repository = repository;
        _passwordHelper = passwordHelper;
    }
    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if(!string.IsNullOrWhiteSpace(request.LoginUserViewModel.PhoneNumber))
        {
            var user = await _repository.GetUserByPhoneNumber(request.LoginUserViewModel.PhoneNumber);
            if(user == null) return LoginUserResult.Success;
            if(user.IsBlocked) return LoginUserResult.IsBlocked;
            if(user.IsDeleted) return LoginUserResult.NotFound;
            if(!user.IsMobileActive) return LoginUserResult.NotActive;
            if(user.Password != _passwordHelper.EncodePasswordMd5(request.LoginUserViewModel.Password)) return LoginUserResult.NotFound;

            return LoginUserResult.Success; 
        }
       return LoginUserResult.NotFound;
    }
}

