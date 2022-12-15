using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.User;
using CleanArchitecture.Infrastructure.Repositories.Entities.User;
using MediatR;

namespace CleanArchitecture.Application.Entities.UserCommands.Create;

public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHelper _passwordHelper;

    public RegisterUserCommandHandler(IUserRepository repository, IPasswordHelper passwordHelper)
    {
        _repository = repository;
        _passwordHelper = passwordHelper;
    }
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if(!await _repository.IsExistsPhoneNumber(request.RegisterUserViewModel.PhoneNumber))
            {
                var user = new Users()
                {
                    FirstName = request.RegisterUserViewModel.FirstName,
                    LastName = request.RegisterUserViewModel.LastName,
                    NationalCode = request.RegisterUserViewModel.NationalCode,
                    Password = _passwordHelper.EncodePasswordMd5(request.RegisterUserViewModel.Password),
                    PhoneNumber = request.RegisterUserViewModel.PhoneNumber,
                    Gender = Gender.Unknown,
                    Email = string.Empty,
                    IsActived = true,
                    IsBlocked = false,
                    IsDeleted = false,
                    Avatar = "",
                    IsMobileActive = false,
                    MobileActiveCode = new Random().Next(1000, 9999).ToString()
                };
                if (user == null)
                    throw new Exception();

                await _repository.AddAsync(user);
                await _repository.Save();
                return RegisterUserResult.Success;
            }

            return RegisterUserResult.MobileExists;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

