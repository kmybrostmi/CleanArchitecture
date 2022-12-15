using CleanArchitecture.Application.Entities.UserCommands;
using CleanArchitecture.Application.Entities.UserCommands.Create;
using CleanArchitecture.Application.Entities.UserCommands.Login;
using CleanArchitecture.Domain.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArchitecture.Endpoint.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public AccountController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel register)
        {
            var result = await _mediator.Send(new RegisterUserCommand(register));

            switch (result)
            {
                case RegisterUserResult.Success:
                    break;
                case RegisterUserResult.MobileExists:
                    break;
                default:
                    break;
            }
            return View(register);
        }


        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel login)
        {
            var result = await _mediator.Send(new LoginUserCommand(login));
            switch (result)
            {
                case LoginUserResult.Success:
                    break;
                case LoginUserResult.IsBlocked:
                    break;
                case LoginUserResult.NotActive:
                    break;
                case LoginUserResult.NotFound:
                    break;
                default:
                    break;
            }

            var user = await _userService.GetUserByPhoneNumber(login.PhoneNumber);
            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.PhoneNumber),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                        };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };
            await HttpContext.SignInAsync(principle, properties);
            return Redirect("/");
        }
    }
}

