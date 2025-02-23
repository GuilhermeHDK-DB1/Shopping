using Microsoft.AspNetCore.Mvc;
using Shopping.web.Models;
using Shopping.web.Services.IServices;

namespace Shopping.web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> ProductIndex()
    {
        var products = await _productService.FindAllAsync();
        return View(products);
    }
    
    public ViewResult ProductCreate()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductModel product)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateAsync(product);

            if (result != null)
                return RedirectToAction(nameof(ProductIndex));
        }

        return View(product);
    }
}