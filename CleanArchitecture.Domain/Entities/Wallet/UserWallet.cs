using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities.Account;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities.Wallet;
public class UserWallet : BaseEntity
{
    [Display(Name = "کاربر")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public Guid UserId { get; set; }

    [Display(Name = "نوع تراکنش")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public WalletType WalletType { get; set; }

    [Display(Name = "مبلغ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int Amount { get; set; }

    [Display(Name = "شرح")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Description { get; set; }

    [Display(Name = "پرداخت شده / نشده")]
    public bool IsPay { get; set; }
    public Users User { get; set; }
}
public enum WalletType
{
    [Display(Name = "واریز")]
    Variz = 1,
    [Display(Name = "برداشت")]
    Bardasht = 2
}