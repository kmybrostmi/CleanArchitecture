

using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using CleanArchitecture.Infrastructure.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Products;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<FilterCategoryViewModel> FilterCategory(FilterCategoryViewModel filter);   
    Task<bool> ExistsCategoryUrl(string url);
    Task<bool> ExistsCategoryUrl(string url, Guid categoryId);
}


