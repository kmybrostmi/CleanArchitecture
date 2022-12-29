using CleanArchitecture.Application.Entities.Roles;
using CleanArchitecture.Application.Entities.User;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Domain.ViewModels.Admin.UserVm;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Endpoint.Areas.Admin.Controllers;

public class UserController : AdminBaseController
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public UserController(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
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

        ViewData["Roles"] = await _roleService.GetAllActiveRoles();
        return View(result);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(EditUserProfileForAdminViewModel userProfileForAdminViewModel)
    {
        ViewData["Roles"] = await _roleService.GetAllActiveRoles();

        userProfileForAdminViewModel.ModifiedBy = User.GetUserId();

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
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                return RedirectToAction("FilterUser");
        }
        return RedirectToAction("FilterUser",viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var modifiedId = User.GetUserId();

        var result = await _userService.RemoveUserForAdmin(userId,modifiedId);
        switch (result)
        {
            case RemoveUserForAdminResult.failed:
                TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                break;
            case RemoveUserForAdminResult.Success:
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                return RedirectToAction("FilterUser");
        }
        return RedirectToAction("FilterUser");
    }
}





