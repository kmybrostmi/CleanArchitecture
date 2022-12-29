
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CleanArchitecture.Domain.ViewModels.Admin.ProductVm;

public class CreateProductViewModel
{
    [Display(Name = "نام محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Name { get; set; }

    [Display(Name = "توضیحات کوتاه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(800, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string ShortDescription { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Description { get; set; }

    [Display(Name = "قیمت محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public long Price { get; set; }

    [Display(Name = "فعال / غیر فعال")]
    public bool IsActive { get; set; }
    public Guid CreateBy { get; set; }

    public List<Guid> ProductCategory { get; set; }
}
public enum CreateProductResult
{
    NotImage,
    Success
}
