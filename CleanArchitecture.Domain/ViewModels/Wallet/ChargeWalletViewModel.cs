using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.ViewModels.Wallet;
public class ChargeWalletViewModel
{
    [Display(Name = "مبلغ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int Amount { get; set; }
}
