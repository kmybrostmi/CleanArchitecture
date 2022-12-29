

using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using CleanArchitecture.Infrastructure.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Products;
public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<Category>> GetAllCategories();
    Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filter);
    Task<Product> GetProductById(Guid productId);

    Task RemoveAllProductCategoryForProduct(Guid productId);
    Task AddSelectedProductCategoryForProduct(List<Guid> selectedProductCategory,Guid productId);
}


