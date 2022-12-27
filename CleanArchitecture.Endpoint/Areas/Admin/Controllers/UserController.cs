using CleanArchitecture.Application.Entities.UserCommands;
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
}



