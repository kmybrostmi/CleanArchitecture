using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanArchitecture.Domain.ViewModels.Admin.ProductVm;

public class EditProductViewModel
{
    public Guid ProductId { get; set; }

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

    public List<Guid> ProductCategory { get; set; }
    public string ProductImageName { get; set; }
    public IFormFile ProductImage { get; set; }
    public Guid ModifiedBy { get; set; }
}

public enum EditProductResult
{
    NotFound,
    NotProductSelectedCategoryHasNull,
    Success
}
