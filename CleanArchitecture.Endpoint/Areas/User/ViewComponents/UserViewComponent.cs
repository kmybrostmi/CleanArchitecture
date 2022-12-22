using CleanArchitecture.Application.Entities.UserCommands;
using CleanArchitecture.Application.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Endpoint.Areas.User.ViewComponents
{
    public class UserSideBarViewComponent : ViewComponent
    {
        #region constrator
        private readonly IUserService _userService;
        public UserSideBarViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserById(User.GetUserId());
                return View("UserSideBar", user);
            }

            return View("UserSideBar");
        }
        #endregion
    }

    public class UserInformationViewComponent : ViewComponent
    {
        #region constrator
        private readonly IUserService _userService;
        public UserInformationViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserById(User.GetUserId());
                return View("UserInformation", user);
            }

            return View("UserInformation");
        }
        #endregion
    }
}
