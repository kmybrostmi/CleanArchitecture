using CleanArchitecture.Domain.Entities.Account;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.ViewModels.Account;

public class EditUserProfileViewModel
{
    [Display(Name = "نام")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string LastName { get; set; }

    [Display(Name = "شماره تلفن همراه")]
    public string PhoneNumber { get; set; }

    [Display(Name = "آواتار")]
    public string Avatar { get; set; }

    [Display(Name = "جنسیت")]
    public Gender UserGender { get; set; }
}
public enum EditUserProfileResult
{
    NotFound,
    Success
}