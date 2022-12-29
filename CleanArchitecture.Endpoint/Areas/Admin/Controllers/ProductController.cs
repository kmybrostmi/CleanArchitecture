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
        TempData["Categories"] = _productService.GetAllProductsCategory();
        return View();
    }

    [HttpPost,AutoValidateAntiforgeryToken]
    public async Task<IActionResult> CreateProduct(CreateProductViewModel productViewModel,IFormFile productImage)
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
}





