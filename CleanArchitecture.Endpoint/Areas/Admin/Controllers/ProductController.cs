using CleanArchitecture.Application.Entities.Products;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Domain.ViewModels.Admin.ProductVm;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Endpoint.Areas.Admin.Controllers;

public class ProductController : AdminBaseController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> FilterProduct(FilterProductsViewModel filterProducts)
    {
        var result = await _productService.FilterProduct(filterProducts);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        TempData["Categories"] = await _productService.GetAllCategories();
        return View();
    }

    [HttpPost, AutoValidateAntiforgeryToken]
    public async Task<IActionResult> CreateProduct(CreateProductViewModel productViewModel, IFormFile productImage)
    {
        //TempData["Categories"] = await _productService.GetAllProductsCategory();

        productViewModel.CreateBy = User.GetUserId();

        var result = await _productService.CreateProduct(productViewModel, productImage);

        switch (result)
        {
            case CreateProductResult.NotImage:
                TempData[ErrorMessage] = "عکس محصول انتخاب نشده";
                break;
            case CreateProductResult.Success:
                TempData[SuccessMessage] = "درج محصول با موفقیت انجام شد";
                return RedirectToAction("FilterProduct");
        }
        return View(productViewModel);
    }


    [HttpGet]
    public async Task<IActionResult> EditProduct(Guid productId)
    {
        var result = await _productService.EditProduct(productId);
        if (result == null)
            return NotFound();

        TempData["Categories"] = await _productService.GetAllCategories();
        return View(result);
    }


    [HttpPost, AutoValidateAntiforgeryToken]
    public async Task<IActionResult> EditProduct(EditProductViewModel productViewModel)
    {
        productViewModel.ModifiedBy = User.GetUserId();

        var result = await _productService.EditProduct(productViewModel);

        switch (result)
        {
            case EditProductResult.NotFound:
                TempData[WarningMessage] = "محصولی با مشخصات وارد شده یافت نشد";
                break;
            case EditProductResult.NotProductSelectedCategoryHasNull:
                TempData[WarningMessage] = "لطفا دسته بندی محصول را وارد کنید";
                break;
            case EditProductResult.Success:
                TempData[SuccessMessage] = "ویرایش محصول با موفقیت انجام شد";
                return RedirectToAction("FilterProduct");
        }
        TempData["Categories"] = await _productService.GetAllCategories();
        return View(productViewModel);
    }


    [HttpGet]
    public async Task<IActionResult> FilterCategory(FilterCategoryViewModel filterCategory)
    {
        var result = await _productService.FilterCategory(filterCategory);
        return View(result);
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View();
    }


    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory(CreateCategoryViewModel createCategoryView, IFormFile image)
    {
        var result = await _productService.CreateCategory(createCategoryView, image);
        switch (result)
        {
            case CreateCategoryResult.IsExistUrlName:
                TempData[WarningMessage] = "اسم Url تکراری است";
                break;
            case CreateCategoryResult.Success:
                TempData[SuccessMessage] = "دسته بندی با موفقیت ثبت شد";
                return RedirectToAction("FilterCategory");
        }
        return View(createCategoryView);
    }


    [HttpGet]
    public async Task<IActionResult> EditCategory(Guid categoryId)
    {
        var result = await _productService.EditCategory(categoryId);
        return View(result);
    }

    [HttpPost , ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCategory(EditCategoryViewModel editCategoryViewModel)
    {
        var result = await _productService.EditCategory(editCategoryViewModel);

        switch (result)
        {
            case EditProductCategoryResult.IsExistUrlName:
                TempData[WarningMessage] = "مسیر انتخاب شده برای دسته بندی از قبل وجود دارد";
                break;
            case EditProductCategoryResult.NotFound:
                TempData[WarningMessage] = "دسته بندی یافت نشد";
                break;
            case EditProductCategoryResult.Success:
                TempData[SuccessMessage] = "دسته بندی با موفقیت ثبت شد";
                return RedirectToAction("FilterCategory");
        }
        return View(editCategoryViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        var result = await _productService.DeleteProduct(productId,User.GetUserId());
        switch (result)
        {
            case DeleteProductResult.NotFound:
                TempData[WarningMessage] = "محصول یافت نشد";
                break;
            case DeleteProductResult.Success:
                TempData[SuccessMessage] = "محصول با موفقیت حذف شد";
                return RedirectToAction("FilterProduct");
        }
        return RedirectToAction("FilterProduct");
    }

    [HttpGet]
    public async Task<IActionResult> RestoreProduct(Guid productId)
    {
        await _productService.RestoreProduct(productId, User.GetUserId());
        return RedirectToAction("FilterProduct");
    }

    [HttpGet]
    public IActionResult GalleryProduct(Guid productId)
    {
        ViewBag.productId = productId;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> AddImagesToProduct(Guid productId, List<IFormFile> images)
    {
        var result = await _productService.AddProductGallery(productId, images);
        if(result)
        {
            JsonResponseStatus.Success();
        }
        JsonResponseStatus.Error();

        return RedirectToAction("FilterProduct");
    }

    public async Task<IActionResult> GetAllProductGalleries(Guid productId)
    {
        var result = await _productService.GetAllProductGalleries(productId);
        return View(result);
    }
}











