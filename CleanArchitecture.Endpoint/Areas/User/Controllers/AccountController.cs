using CleanArchitecture.Application.Entities.UserCommands;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Endpoint.Areas.User.Controllers;

public class AccountController : UserBaseController
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("edit-user-profile")]
    public async Task<IActionResult> EditUserProfile()
    {
        var user = await _userService.EditUserProfileData(User.GetUserId());
        if (user == null)
            return NotFound();
        return View(user);
    }

    [HttpPost("edit-user-profile")]
    public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editUserProfile, IFormFile userAvatar)
    {
        var result = await _userService.EditUserProfile(User.GetUserId(), userAvatar, editUserProfile);
        switch (result)
        {
            case EditUserProfileResult.NotFound:
                TempData[WarningMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                break;
            case EditUserProfileResult.Success:
                TempData[SuccessMessage] = "عملیات ویرایش حساب کاربری با موفقیت انجام شد";
                return View(editUserProfile);
        }
        return View(editUserProfile);
    }
}
