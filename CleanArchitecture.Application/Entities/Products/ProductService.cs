using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using CleanArchitecture.Infrastructure.Repositories.Entities.Products;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Entities.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductService(IProductRepository repository,IProductCategoryRepository productCategoryRepository)
	{
        _repository = repository;
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<CreateProductResult> CreateProduct(CreateProductViewModel productViewModel, IFormFile productImage)
    {
        var product = new Product
        {
            Name = productViewModel.Name,
            CreateById = productViewModel.CreateBy,
            CreateDate = DateTime.Now,
            Description = productViewModel.Description,
            ShortDescription = productViewModel.ShortDescription,
            IsActive = productViewModel.IsActive,
            IsActived = true,
            IsDeleted = false,
            Price = productViewModel.Price
        };

        if (productImage != null && productImage.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productImage.FileName);
            productImage.AddImageToServer(imageName, PathExtensions.ProductOrginServer, 255, 273, PathExtensions.ProductThumbServer);
            product.ProductImageName = imageName;
        }
        else
        {
            return CreateProductResult.NotImage;
        }

        await _repository.AddAsync(product);
        await _repository.Save();
        return CreateProductResult.Success;
    }


    public async Task<CreateProductCategoryResult> CreateProductCategory(CreateProductCategoryViewModel productViewModel, IFormFile productCategoryImage)
    {
        //if (await _productRepository.CheckUrlNameCategories(createCategory.UrlName)) return CreateProductCategoryResult.IsExistUrlName;

        var productCategory = new Category
        {
            Title = productViewModel.Title,
            CreateById = productViewModel.CreateBy,
            CreateDate = DateTime.Now,
            IsActived = true,
            IsDeleted = false,
            UrlName = productViewModel.UrlName,
        };

        if (productCategoryImage != null && productCategoryImage.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productCategoryImage.FileName);
            productCategoryImage.AddImageToServer(imageName, PathExtensions.CategoryOrginServer, 150, 150, PathExtensions.CategoryThumbServer);
            productCategory.ImageName = imageName;
        }

        await _productCategoryRepository.AddAsync(productCategory);
        await _productCategoryRepository.Save();
        return CreateProductCategoryResult.Success;
    }

    public async Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filterProducts)
    {
        return await _repository.FilterProduct(filterProducts); 
    }

    public async Task<List<ProductCategory>> GetAllProductsCategory()
    {
        return await _repository.GetAllProductsCategory();
    }
}

