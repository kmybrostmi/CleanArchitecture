

using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using CleanArchitecture.Infrastructure.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Products;
public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<ProductCategory>> GetAllProductsCategory();
    Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filter);
}
