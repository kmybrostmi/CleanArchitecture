

using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Infrastructure.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Products;

public interface IProductCategoryRepository : IBaseRepository<Category>
{
}
