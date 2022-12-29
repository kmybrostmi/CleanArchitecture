

using CleanArchitecture.Domain.Common.Paging;
using CleanArchitecture.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
public class FilterProductsViewModel : BasePaging
{
    public string SearchTerm { get; set; }
    public string FilterByCategory { get; set; }
    public List<Product> Products { get; set; }
    public ProductState ProductState { get; set; }
    public ProductOrder ProductOrder { get; set; }

    public FilterProductsViewModel SetProducts(List<Product> products)
    {
        this.Products = products;
        return this;
    }

    public FilterProductsViewModel SetPaging(BasePaging paging)
    {
        this.PageId = paging.PageId;
        this.AllEntityCount = paging.AllEntityCount;
        this.StartPage = paging.StartPage;
        this.EndPage = paging.EndPage;
        this.TakeEntity = paging.TakeEntity;
        this.CountForShowAfterAndBefor = paging.CountForShowAfterAndBefor;
        this.SkipEntitiy = paging.SkipEntitiy;
        this.PageCount = paging.PageCount;

        return this;
    }
}
public enum ProductState
{
    [Display(Name = "همه")]
    All,

    [Display(Name = "فعال")]
    IsActice,

    [Display(Name = "حذف شده")]
    Delete
}
public enum ProductOrder
{
    [Display(Name = "همه")]
    All,

    [Display(Name = "جدیدترین ها")]
    ProductNewss,

    [Display(Name = "گران ترین ها")]
    ProductExp,

    [Display(Name = "ارزان ترین ها")]
    ProductInExprnsive
}


