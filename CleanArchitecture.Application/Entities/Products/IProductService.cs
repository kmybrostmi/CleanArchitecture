using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Entities.Products;

public interface IProductService
{
    Task<List<ProductCategory>> GetAllProductsCategory();

    Task<CreateProductResult> CreateProduct(CreateProductViewModel productViewModel, IFormFile productImage);
    Task<CreateProductCategoryResult> CreateProductCategory(CreateProductCategoryViewModel productViewModel, IFormFile productCategoryImage);


    Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filterProducts);
}


