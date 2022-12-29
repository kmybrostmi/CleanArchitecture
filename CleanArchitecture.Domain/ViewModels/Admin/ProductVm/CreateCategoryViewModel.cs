
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.ViewModels.Admin.ProductVm;

public class CreateCategoryViewModel
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Title { get; set; }

    [Display(Name = "عنوان url")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string UrlName { get; set; }
    public Guid CreateBy { get; set; }

    //[Display(Name = "تصویر")]
    //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    //[MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    //public string ImageName { get; set; }

}
public enum CreateCategoryResult
{
    IsExistUrlName,
    Success
}

