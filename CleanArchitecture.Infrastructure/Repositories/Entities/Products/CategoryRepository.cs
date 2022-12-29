

using CleanArchitecture.Domain.Common.Paging;
using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Products;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<FilterCategoryViewModel> FilterCategory(FilterCategoryViewModel filter)
    {
        var query = Context.Categories.Where(x => x.IsActived && !x.IsDeleted);

        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            query = query.Where(x => x.Title.Contains(filter.SearchTerm));
        }

        var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
        var allData = await query.Paging(pager).ToListAsync();

        return filter.SetPaging(pager).SetProductCategories(allData);
    }
}