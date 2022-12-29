using CleanArchitecture.Application.Entities.User;
using CleanArchitecture.Application.Entities.UserWallets;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Domain.ViewModels.Account;
using CleanArchitecture.Domain.ViewModels.Wallet;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarinpalSandbox;

namespace CleanArchitecture.Endpoint.Areas.User.Controllers;

public class AccountController : UserBaseController
{
    private readonly IUserService _userService;
    private readonly IUserWalletService _walletService;
    private readonly IConfiguration _configuration;

    public AccountController(IUserService userService, IUserWalletService walletService, IConfiguration configuration)
    {
        _userService = userService;
        _walletService = walletService;
        _configuration = configuration;
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



    [HttpGet("change-password")]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost("change-password"), ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
    {
        var result = await _userService.ChangUserPassword(User.GetUserId(), changePassword);
        switch (result)
        {
            case ChangePasswordResult.NotFound:
                TempData[WarningMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                break;
            case ChangePasswordResult.PasswordEqual:
                TempData[InfoMessage] = "لطفا از کلمه عبور جدیدی استفاده کنید";
                ModelState.AddModelError("NewPassword", "لطفا از کلمه عبور جدیدی استفاده کنید");
                break;
            case ChangePasswordResult.Success:
                TempData[SuccessMessage] = "کلمه ی عبور شما با موفقیت تغیر یافت";
                TempData[InfoMessage] = "لطفا جهت تکمیل فراید تغیر کلمه ی عبور ،مجددا وارد سایت شود";
                await HttpContext.SignOutAsync();
                return RedirectToAction("Login", "Account", new { area = "" });
        }
        TempData[ErrorMessage] = "کلمه عبور وارد شده مطابقت ندارد";
        return View(changePassword);
    }


    [HttpGet("charge-wallet")]
    public async Task<IActionResult> ChargeWallet()
    {
        return View();
    }

    [HttpPost("charge-wallet"), ValidateAntiForgeryToken]
    public async Task<IActionResult> ChargeWallet(ChargeWalletViewModel chargeWallet)
    {
        var walletId = await _walletService.ChargeWallet(User.GetUserId(), User.GetUserId(), chargeWallet, $"شارژ به مبلغ {chargeWallet.Amount}");

        //payment
        var payment = new Payment(chargeWallet.Amount);
        var url = _configuration.GetSection("DefaultUrl")["Host"] + "/user/online-payment/" + walletId;
        var result = payment.PaymentRequest("شارژ کیف پول", url);

        if (result.Result.Status == 100)
        {
            return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
        }
        else
        {
            TempData[ErrorMessage] = "مشکلی در پرداخت به وجود آماده است،لطفا مجددا امتحان کنید";
        }
        return View(chargeWallet);
    }

    [HttpGet("online-payment/{id}")]
    public async Task<IActionResult> OnlinePayment(Guid id)
    {
        if (HttpContext.Request.Query["Status"] != "" && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" && HttpContext.Request.Query["Authority"] != "")
        {
            string authority = HttpContext.Request.Query["Authority"];
            var wallet = await _walletService.GetUserWalletById(id);
            if (wallet != null)
            {
                var payment = new Payment(wallet.Amount);
                var result = payment.Verification(authority).Result;

                if (result.Status == 100)
                {
                    ViewBag.RefId = result.RefId;
                    ViewBag.Success = true;
                    await _walletService.UpdateWalletForCharge(wallet);
                }
                return View();
            }
            return NotFound();
        }
        return View();
    }

    [HttpGet("user-wallet")]
    public async Task<IActionResult> UserWallet(FilterWalletViewModel filter)
    {
        filter.UserId = User.GetUserId();
        return View(await _walletService.FilterWallets(filter));
    }
}
