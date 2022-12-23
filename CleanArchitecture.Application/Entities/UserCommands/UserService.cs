using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.Helpers.Interfaces;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Account;
using CleanArchitecture.Infrastructure.Repositories.Entities.User;
using Microsoft.AspNetCore.Http;

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

        if (user.MobileActiveCode == loginUser.ActiveCode)
        {
            //user.MobileActiveCode = new Random().Next(1000, 9999).ToString();
            user.IsMobileActive = true;
            _repository.UpdateUser(user);
            await _repository.Save();
            return ActiveAccountResult.Success;
        }

        return ActiveAccountResult.Error;
    }

    public async Task<ChangePasswordResult> ChangUserPassword(Guid userId, ChangePasswordViewModel changePassword)
    {
        var user = await _repository.GetUserById(userId);
        if (user == null) return ChangePasswordResult.NotFound;

        if (user.Password == _passwordHelper.EncodePasswordMd5(changePassword.CurrentPassword))
        {
            if (_passwordHelper.EncodePasswordMd5(changePassword.ConfirmNewPassword) == _passwordHelper.EncodePasswordMd5(changePassword.NewPassword))
            {
                user.Password = _passwordHelper.EncodePasswordMd5(changePassword.NewPassword);
                _repository.Update(user);
                await _repository.Save();
                return ChangePasswordResult.Success;
            }
        }
        return ChangePasswordResult.NotMatched;
    }

    public async Task<EditUserProfileResult> EditUserProfile(Guid userId, IFormFile userAvatar, EditUserProfileViewModel editUser)
    {
        var user = await _repository.GetUserById(userId);
        if (user == null)
            return EditUserProfileResult.NotFound;

        user.FirstName = editUser.FirstName;
        user.LastName = editUser.LastName;
        user.Gender = editUser.UserGender;

        if (userAvatar.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(userAvatar.FileName);
            userAvatar.AddImageToServer(imageName, PathExtensions.UserAvatarOrginServer, 150, 150, PathExtensions.UserAvatarThumbServer);

            user.Avatar = imageName;
        }

        _repository.UpdateUser(user);
        await _repository.Save();

        return EditUserProfileResult.Success;
    }

    public async Task<EditUserProfileViewModel> EditUserProfileData(Guid userId)
    {
        var user = await _repository.GetUserById(userId);
        if (user == null)
            throw new Exception();

        return new EditUserProfileViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            UserGender = user.Gender,
            Avatar = user.Avatar,
        };
    }

    public async Task<Users> GetUserById(Guid id)
    {
        var user = await _repository.GetUserById(id);
        return user;
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
            if (user.Password != _passwordHelper.EncodePasswordMd5(loginUser.Password)) return LoginUserResult.UserNameOrPasswordIsIncorrect;

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
