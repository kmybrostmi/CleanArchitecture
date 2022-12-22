﻿using CleanArchitecture.Domain.ViewModels.Site;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.ViewModels.Account;

public class ActiveAccountViewModel : Recaptcha
{
    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string PhoneNumber { get; set; }

    [Display(Name = "کد فعال سازی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string ActiveCode { get; set; }
}

public enum ActiveAccountResult
{
    Error,
    Success,
    NotFound
}
