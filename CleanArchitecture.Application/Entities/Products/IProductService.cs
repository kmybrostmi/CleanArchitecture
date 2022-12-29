using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Entities.Products;

public interface IProductService
{
    Task<List<ProductCategory>> GetAllProductsCategory();

    Task<CreateProductResult> CreateProduct(CreateProductViewModel productViewModel, IFormFile productImage);
    Task<CreateCategoryResult> CreateCategory(CreateCategoryViewModel productViewModel, IFormFile productCategoryImage);

    Task<EditProductResult> EditProduct(EditProductViewModel productViewModel);
    Task<EditProductViewModel> EditProduct(Guid productId);
    Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filterProducts);



    Task<FilterCategoryViewModel> FilterCategory(FilterCategoryViewModel filter);
}


