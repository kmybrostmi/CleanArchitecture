

using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Infrastructure.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Products;

public interface IProductGalleryRepository : IBaseRepository<ProductGalleries>
{
    Task<List<ProductGalleries>> GetAllProductGalleries(Guid productId); 
}






