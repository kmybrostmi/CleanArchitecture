using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Entities.Products;

public interface IProductService
{
    Task<CreateProductResult> CreateProduct(CreateProductViewModel productViewModel, IFormFile productImage);
    Task<EditProductResult> EditProduct(EditProductViewModel productViewModel);
    Task<EditProductViewModel> EditProduct(Guid productId);
    Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filterProducts);
    Task<DeleteProductResult> DeleteProduct(Guid productId, Guid modifiedBy);
    Task RestoreProduct(Guid productId, Guid modifiedBy);
    Task<bool> AddProductGallery(Guid productId, List<IFormFile> images);

    //Category
    Task<List<Category>> GetAllCategories();
    Task<EditCategoryViewModel> EditCategory(Guid categoryId);
    Task<EditProductCategoryResult> EditCategory(EditCategoryViewModel editCategoryViewModel);
    Task<CreateCategoryResult> CreateCategory(CreateCategoryViewModel categoryViewModel, IFormFile productCategoryImage);
    Task<FilterCategoryViewModel> FilterCategory(FilterCategoryViewModel filter);
}



