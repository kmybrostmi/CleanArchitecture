using CleanArchitecture.Domain.Common.Paging;
using CleanArchitecture.Domain.Entities.Products;

namespace CleanArchitecture.Domain.ViewModels.Admin.ProductVm;

public class FilterCategoryViewModel : BasePaging
{
    public string SearchTerm { get; set; }

    public List<Category> Categories { get; set; }


    public FilterCategoryViewModel SetProductCategories(List<Category> categories)
    {
        this.Categories = categories;
        return this;
    }

    public FilterCategoryViewModel SetPaging(BasePaging paging)
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
