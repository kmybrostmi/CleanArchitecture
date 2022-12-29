using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities.Products;
public class Product:BaseEntity
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

    [Display(Name = "تصویر محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string ProductImageName { get; set; }

    [Display(Name = "فعال / غیر فعال")]
    public bool IsActive { get; set; }

    public ICollection<ProductFeature> ProductFeatures { get; set; }
    public ICollection<ProductGalleries> Galleries { get; set; }
    public ICollection<ProductCategory> ProductsCategories { get; set; }   
}






