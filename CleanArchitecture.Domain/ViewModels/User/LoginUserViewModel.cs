﻿using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.ViewModels.User;
public class LoginUserViewModel
{
    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string PhoneNumber { get; set; }
    [Display(Name = "رمزعبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Password { get; set; }
    [Display(Name = "مرا بخاطر بسپار")]
    public bool RememberMe { get; set; }
}


public enum LoginUserResult
{
    Success,
    IsBlocked,
    NotActive,
    NotFound
}