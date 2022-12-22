using Azure.Core;
using CleanArchitecture.Application.Helpers.Interfaces;
using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Account;
using CleanArchitecture.Infrastructure.Repositories.Entities.User;
using MediatR;

namespace CleanArchitecture.Application.Entities.UserCommands;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHelper _passwordHelper;
    private readonly ISmsService _smsService;

    public UserService(IUserRepository repository, IPasswordHelper passwordHelper, ISmsService smsService)
    {
        _repository = repository;
        _passwordHelper = passwordHelper;
        _smsService = smsService;
    }

    public async Task<ActiveAccountResult> ActiveAccount(ActiveAccountViewModel loginUser)
    {
        var user = await _repository.GetUserByPhoneNumber(loginUser.PhoneNumber);
        if (user == null)
            return ActiveAccountResult.NotFound;

        if(user.MobileActiveCode == loginUser.ActiveCode)
        {
            //user.MobileActiveCode = new Random().Next(1000, 9999).ToString();
            user.IsMobileActive = true;
            _repository.UpdateUser(user);
            await _repository.Save();
            return ActiveAccountResult.Success;
        }

        return ActiveAccountResult.Error;
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

    public async Task<RegisterUserResult> RegisterUser(RegisterUserViewModel loginUser)
    {
        try
        {
            if (!await _repository.IsExistsPhoneNumber(loginUser.PhoneNumber))
            {
                var user = new Users()
                {
                    FirstName = loginUser.FirstName,
                    LastName = loginUser.LastName,
                    NationalCode = loginUser.NationalCode,
                    Password = _passwordHelper.EncodePasswordMd5(loginUser.Password),
                    PhoneNumber = loginUser.PhoneNumber,
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
                await _smsService.SendVirificationCode(user.PhoneNumber, user.MobileActiveCode);
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
