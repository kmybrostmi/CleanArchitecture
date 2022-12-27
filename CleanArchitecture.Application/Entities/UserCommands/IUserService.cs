using CleanArchitecture.Application.Entities.UserCommands.Login;
using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Account;
using CleanArchitecture.Domain.ViewModels.Admin.UserVm;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Entities.UserCommands;

public interface IUserService
{
    Task<Users> GetUserByPhoneNumber(string phoneNumber);
    Task<LoginUserResult> LoginUser(LoginUserViewModel loginUser);
    Task<RegisterUserResult> RegisterUser(RegisterUserViewModel loginUser);
    Task<ActiveAccountResult> ActiveAccount(ActiveAccountViewModel loginUser);
    Task<Users> GetUserById(Guid id);

    //Users
    Task<EditUserProfileViewModel> EditUserProfileData(Guid userId);
    Task<EditUserProfileResult> EditUserProfile(Guid userId, IFormFile userAvatar, EditUserProfileViewModel editUser);
    Task<ChangePasswordResult> ChangUserPassword(Guid userId, ChangePasswordViewModel changePassword); 

    //Admin
    Task<FilterUserViewModel> FilterUser(FilterUserViewModel filterUser);   
}


