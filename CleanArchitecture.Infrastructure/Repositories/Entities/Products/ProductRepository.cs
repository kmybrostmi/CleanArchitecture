using CleanArchitecture.Domain.Common.Paging;
using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Products;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddSelectedProductCategoryForProduct(List<Guid> selectedProductCategory, Guid productId)
    {
        if(selectedProductCategory!=null && selectedProductCategory.Any())
        {
            var productCategories = new List<ProductCategory>();

            foreach (var categoryId in selectedProductCategory)
            {
                productCategories.Add(new ProductCategory
                {
                    ProductId = productId,
                    CategoryId = categoryId
                });
            }

            await Context.ProductsCategories.AddRangeAsync(productCategories);
            await Context.SaveChangesAsync(); 
        }
    }

    public async Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filter)
    {
        var query = Context.Products
                           .Where(x => x.IsActived && !x.IsDeleted)
                           .Include(x => x.ProductsCategories.Where(x => x.IsActived && !x.IsDeleted))
                           .ThenInclude(x => x.Category)
                           .AsQueryable();

        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchTerm) ||
                                   x.ShortDescription.Contains(filter.SearchTerm));

        }

        if (!string.IsNullOrEmpty(filter.FilterByCategory))
        {
            query = query.Where(c => c.ProductsCategories.Any(s => s.Category.UrlName == filter.FilterByCategory));
        }

        switch (filter.ProductState)
        {
            case ProductState.All:
                break;
            case ProductState.IsActice:
                query = query.Where(c => c.IsActive);
                break;
            case ProductState.Delete:
                query = query.Where(c => c.IsDeleted);
                break;
        }

        switch (filter.ProductOrder)
        {
            case ProductOrder.All:
                break;
            case ProductOrder.ProductNewss:
                query = query.Where(c => c.IsActive).OrderByDescending(c => c.CreateDate);
                break;
            case ProductOrder.ProductExp:
                query = query.Where(c => c.IsActive).OrderByDescending(c => c.Price);
                break;
            case ProductOrder.ProductInExprnsive:
                query = query.Where(c => c.IsActive).OrderBy(c => c.Price);
                break;
        }

        var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
        var allData = await query.Paging(pager).ToListAsync();

        return filter.SetPaging(pager).SetProducts(allData);
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await Context.Categories.Where(x => x.IsActived && !x.IsDeleted).ToListAsync();
    }

    public async Task<Product> GetProductById(Guid productId)
    {
        return await Context.Products.Include(x=>x.ProductsCategories).ThenInclude(x=>x.Category).SingleOrDefaultAsync(x => x.Id == productId);
    }

    public async Task RemoveAllProductCategoryForProduct(Guid productId)
    {
        var product = await Context.Products.FirstOrDefaultAsync(x => x.Id == productId);

        if (product.ProductsCategories != null && product.ProductsCategories.Any())
        {
            Context.ProductsCategories.RemoveRange(product.ProductsCategories);
        }
    }
}
