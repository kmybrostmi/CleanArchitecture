using CleanArchitecture.Application.Entities.UserCommands;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Domain.ViewModels.Admin.UserVm;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Endpoint.Areas.Admin.Controllers;

public class UserController : AdminBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<IActionResult> FilterUser(FilterUserViewModel filterUser)
    {
        var result = await _userService.FilterUser(filterUser);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(Guid userId)
    {
        var result = await _userService.EditUserForAdmin(userId);
        if (result == null)
        {
            return View();
        }
        return View(result);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(EditUserProfileForAdminViewModel userProfileForAdminViewModel)
    {
        var result = await _userService.EditUserForAdmin(userProfileForAdminViewModel);

        switch (result)
        {
            case EditUserFromAdminResult.NotFound:
                TempData[WarningMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                break;
            case EditUserFromAdminResult.Success:
                TempData[SuccessMessage] = "ویراش کاربر با موفقیت انجام شد";
                return RedirectToAction("FilterUser");
        }

        return View(userProfileForAdminViewModel);
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost,ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(CreateUserForAdminViewModel viewModel)
    {
        viewModel.CreateBy = User.GetUserId();

        var result = await _userService.CreateUserForAdmin(viewModel);

        switch (result)
        {
            case CreateUserForAdminResult.MobileExists:
                TempData[ErrorMessage] = "شماره تلفن وارد شده قبلا در سیستم ثبت شده";
                break;
            case CreateUserForAdminResult.NationalCodeExists:
                TempData[ErrorMessage] = "کد ملی وارد شده قبلا در سیستم ثبت شده";
                break;
            case CreateUserForAdminResult.failure:
                TempData[ErrorMessage] = "خطایی در انجام عملیات رخ داد";
                break;
            case CreateUserForAdminResult.success:
                TempData[SuccessMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                return RedirectToAction("FilterUser");
        }
        return RedirectToAction("FilterUser",viewModel);
    }
}



