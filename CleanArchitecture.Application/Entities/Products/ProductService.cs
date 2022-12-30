using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using CleanArchitecture.Infrastructure.Repositories.Entities.Products;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Entities.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository repository,ICategoryRepository categoryRepository)
	{
        _repository = repository;
        _categoryRepository = categoryRepository;
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


    public async Task<CreateCategoryResult> CreateCategory(CreateCategoryViewModel categoryViewModel, IFormFile productCategoryImage)
    {
        if (await _categoryRepository.ExistsCategoryUrl(categoryViewModel.UrlName)) return CreateCategoryResult.IsExistUrlName;

        var productCategory = new Category
        {
            Title = categoryViewModel.Title,
            CreateById = categoryViewModel.CreateBy,
            CreateDate = DateTime.Now,
            IsActived = true,
            IsDeleted = false,
            UrlName = categoryViewModel.UrlName,
            ParentId = null
        };

        if (productCategoryImage != null && productCategoryImage.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productCategoryImage.FileName);
            productCategoryImage.AddImageToServer(imageName, PathExtensions.CategoryOrginServer, 150, 150, PathExtensions.CategoryThumbServer);
            productCategory.ImageName = imageName;
        }

        await _categoryRepository.AddAsync(productCategory);
        await _categoryRepository.Save();
        return CreateCategoryResult.Success;
    }

    public async Task<EditProductResult> EditProduct(EditProductViewModel productViewModel)
    {
        var product = await _repository.GetProductById(productViewModel.ProductId);
        if (product == null) return EditProductResult.NotFound;

        product.Name = productViewModel.Name;
        product.Price = productViewModel.Price;
        product.ShortDescription = productViewModel.ShortDescription;
        product.Description = productViewModel.Description;
        product.IsActive = productViewModel.IsActive;
        product.ModifiedDate = DateTime.Now;
        product.ModifiedById = productViewModel.ModifiedBy;

        if (productViewModel.ProductImage != null && productViewModel.ProductImage.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productViewModel.ProductImage.FileName);
            productViewModel.ProductImage.AddImageToServer(imageName, PathExtensions.ProductOrginServer, 255, 273, PathExtensions.ProductThumbServer, product.ProductImageName);
            product.ProductImageName = imageName;
        }

        await _repository.RemoveAllProductCategoryForProduct(product.Id);
        await _repository.AddSelectedProductCategoryForProduct(productViewModel.ProductCategory, product.Id);

        product.ProductsCategories.First(x => x.ProductId == product.Id).CreateDate = DateTime.Now;
        product.ProductsCategories.First(x => x.ProductId == product.Id).CreateById = productViewModel.ModifiedBy;

        _repository.Update(product);
        await _repository.Save();
        return EditProductResult.Success;

    }

    public async Task<EditProductViewModel> EditProduct(Guid productId)
    {
        var product = await _repository.GetProductById(productId);

        if(product != null)
        {
            return new EditProductViewModel
            {
                Name = product.Name,
                Price = product.Price,
                IsActive = product.IsActive,
                ProductImageName = product.ProductImageName,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                ProductCategory = product.ProductsCategories.Where(x => x.ProductId == product.Id).Select(x=>x.Id).ToList()
            };
        }
        return null;
    }

    public async Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filterProducts)
    {
        return await _repository.FilterProduct(filterProducts); 
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _repository.GetAllCategories();
    }

    public async Task<FilterCategoryViewModel> FilterCategory(FilterCategoryViewModel filter)
    {
       return await _categoryRepository.FilterCategory(filter); 
    }

    public async Task<EditCategoryViewModel> EditCategory(Guid categoryId)
    {
        var category = await _categoryRepository.GetTracking(categoryId);

        if(category != null)
        {
            return new EditCategoryViewModel
            {
                ImageName= category.ImageName,
                Title= category.Title,  
                UrlName= category.UrlName,
            };
        }
        return null;
    }

    public async Task<EditProductCategoryResult> EditCategory(EditCategoryViewModel editCategoryViewModel)
    {
        var category = await _categoryRepository.GetTracking(editCategoryViewModel.CategoryId);
        if (category == null) return EditProductCategoryResult.NotFound;

        if (await _categoryRepository.ExistsCategoryUrl(editCategoryViewModel.UrlName, editCategoryViewModel.CategoryId)) 
            return EditProductCategoryResult.IsExistUrlName;

        category.ModifiedDate = DateTime.Now;
        category.ModifiedById = editCategoryViewModel.ModifiedBy;
        category.Title = editCategoryViewModel.Title;
        category.UrlName = editCategoryViewModel.UrlName;

        if (editCategoryViewModel.CategoryImage != null && editCategoryViewModel.CategoryImage.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editCategoryViewModel.CategoryImage.FileName);
            editCategoryViewModel.CategoryImage.AddImageToServer(imageName, PathExtensions.ProductOrginServer, 255, 273, PathExtensions.ProductThumbServer, category.ImageName);
            category.ImageName = imageName;
        }

        //_categoryRepository.Update(category);
        await _categoryRepository.Save();
        return EditProductCategoryResult.Success;
    }
}

