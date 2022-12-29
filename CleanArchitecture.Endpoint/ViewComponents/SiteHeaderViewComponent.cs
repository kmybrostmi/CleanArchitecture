using CleanArchitecture.Application.Entities.User;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Endpoint.ViewComponents;
public class SiteHeaderViewComponent : ViewComponent
{
    private readonly IUserService _userService;

    public SiteHeaderViewComponent(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (User.Identity.IsAuthenticated)
        {
            ViewBag.User = await _userService.GetUserByPhoneNumber(User.Identity.Name);
        }
        return View("SiteHeader");
    }
}
