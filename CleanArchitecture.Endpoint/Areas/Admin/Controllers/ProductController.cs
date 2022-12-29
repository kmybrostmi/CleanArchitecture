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
        TempData["Categories"] = _productService.GetAllCategories();
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
        TempData["Categories"] = await _productService.GetAllCategories();

        var result = await _productService.EditProduct(productId);
        return View(result);
    }


    [HttpPost, AutoValidateAntiforgeryToken]
    public async Task<IActionResult> EditProduct(EditProductViewModel productViewModel)
    {
        //TempData["Categories"] = await _productService.GetAllCategories();
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
        return View(productViewModel);
    }


    [HttpGet]
    public async Task<IActionResult> FilterCategory(FilterCategoryViewModel filterCategory)
    {
        var result = await _productService.FilterCategory(filterCategory);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateCategory()
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
}







