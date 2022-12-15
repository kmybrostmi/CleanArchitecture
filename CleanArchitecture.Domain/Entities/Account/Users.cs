using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities.Account;
public class Users : BaseEntity
{

    [Display(Name ="نام")]
    [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
    [MaxLength(50,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string FirstName { get; set; }
    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string LastName { get; set; }
    [Display(Name = "کد ملی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(10,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string NationalCode { get; set; }
    [Display(Name = "آواتار")]
    [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
    [MaxLength(50,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Avatar { get; set; }
    [Display(Name = "رمزعبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Password { get; set; }
    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string PhoneNumber { get; set; }
    [Display(Name = "ایمیل")]
    [MaxLength(50,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Email { get; set; }
    [Display(Name = "مسدود شده / نشده")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public bool IsBlocked { get; set; } = false;
    [Display(Name = "کد احراز هویت")]
    [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
    [MaxLength(50,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string MobileActiveCode { get; set; }
    [Display(Name = "تایید شده / نشده")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public bool IsMobileActive { get; set; } = false;
    [Display(Name = "جنسیت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public Gender Gender { get; set; } = Gender.Unknown;
}




