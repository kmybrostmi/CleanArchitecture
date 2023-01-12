using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Products;

public class ProductGalleryRepository : BaseRepository<ProductGalleries>, IProductGalleryRepository
{
    public ProductGalleryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<ProductGalleries>> GetAllProductGalleries(Guid productId)
    {
        return await Context.ProductGalleries.Where(x=>x.ProductId == productId).ToListAsync(); 
    }
}