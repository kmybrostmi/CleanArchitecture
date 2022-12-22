using CleanArchitecture.Application.Entities.UserCommands;
using CleanArchitecture.Application.Entities.UserCommands.Create;
using CleanArchitecture.Application.Entities.UserCommands.Login;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CleanArchitecture.Domain.ViewModels.Account;
using GoogleReCaptcha.V3.Interface;
using Microsoft.Win32;

namespace CleanArchitecture.Endpoint.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;

        public AccountController(IMediator mediator, IUserService userService, ICaptchaValidator captchaValidator)
        {
            _mediator = mediator;
            _userService = userService;
            _captchaValidator = captchaValidator;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel register)
        {
            if(!await _captchaValidator.IsCaptchaPassedAsync(register.Token))
            {
                TempData[ErrorMessage] = "کد کپچا معتبر نیست";
                return View(register);
            }
            var result = await _userService.RegisterUser(register);

            switch (result)
            {
                case RegisterUserResult.Success:
                    TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد";
                    break;
                case RegisterUserResult.MobileExists:
                    TempData[ErrorMessage] = "شماره تلفن وارد شده قبلا در سیستم ثبت شده است";
                    return Redirect("/");
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
            if (!await _captchaValidator.IsCaptchaPassedAsync(login.Token))
            {
                TempData[ErrorMessage] = "کد کپچا معتبر نیست";
                return View(login);
            }
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUser(login);
                switch (result)
                {
                    case LoginUserResult.NotFound:
                        TempData[WarningMessage] = "حساب کاربری یافت نشد";
                        break;
                    case LoginUserResult.IsBlocked:
                        TempData[ErrorMessage] = "حساب کاربری شما مسدود شده است";
                        break;
                    case LoginUserResult.NotActive:
                        TempData[ErrorMessage] = "حساب کاربری شما فعال نیست";
                        break;
                    case LoginUserResult.Success:
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
                        TempData[SuccessMessage] = "با موفقیت وارد شدید";
                        return Redirect("/");
                }
            }
            return View(login);
        }

        [HttpGet("log-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            TempData[SuccessMessage] = "با موفقیت از حساب کاربری خارج شدید";
            return Redirect("/");
        }
    }
}

