using CleanArchitecture.Application.Entities.RolesCommands;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Domain.ViewModels.Admin.RoleVm;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Endpoint.Areas.Admin.Controllers;

public class RoleController : AdminBaseController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> FilterRoles(FilterRoleViewModel viewModel)
    {
        var result = await _roleService.FilterRole(viewModel);
        return View(result);
    }



    [HttpGet]
    public async Task<IActionResult> EditRole(Guid roleId)
    {
        ViewData["Permissions"] = await _roleService.GetAllActiveRolePermission();
        var result = await _roleService.GetRoleById(roleId);
        //result.SelectedPermissions = new List<Guid> { Guid.Parse("E6CD6056-0EB4-4D67-B5AD-8D2E69C83637") } ;
        return View(result);
    }


    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(CreateOrEditRoleViewModel viewModel)
    {
        ViewData["Permissions"] = await _roleService.GetAllActiveRolePermission();

        viewModel.ModifiedBy = User.GetUserId();

        var result = await _roleService.CreateOrEditRoleForAdmin(viewModel);

        switch (result)
        {
            case CreateOrEditRoleResult.NotFound:
                TempData[ErrorMessage] = "نقش یافت نشد";
                break;

            case CreateOrEditRoleResult.NotExistPermissions:
                TempData[WarningMessage] = "دسترسی های نقش انتخاب نشده";
                break;

            case CreateOrEditRoleResult.Success:
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                return RedirectToAction("FilterRoles");
        }
        return RedirectToAction("FilterRoles");
    }






    [HttpGet]
    public async Task<IActionResult> CreateRoleLoad()
    {
        ViewData["Permissions"] = await _roleService.GetAllActiveRolePermission();
        return View();
    }


    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole(CreateOrEditRoleViewModel viewModel)
    {
        ViewData["Permissions"] = await _roleService.GetAllActiveRolePermission();

        viewModel.CreateBy = User.GetUserId();

        var result = await _roleService.CreateOrEditRoleForAdmin(viewModel);

        switch (result)
        {
            case CreateOrEditRoleResult.NotFound:
                TempData[ErrorMessage] = "نقش یافت نشد";
                break;

            case CreateOrEditRoleResult.NotExistPermissions:
                TempData[WarningMessage] = "دسترسی های نقش انتخاب نشده";
                break;

            case CreateOrEditRoleResult.Success:
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                return RedirectToAction("FilterRoles");
        }
        return RedirectToAction("FilterRoles");
    }
}




