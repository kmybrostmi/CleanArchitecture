using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Products;

public class ProductGalleryRepository : BaseRepository<ProductGalleries>, IProductGalleryRepository
{
    public ProductGalleryRepository(AppDbContext context) : base(context)
    {
    }
}