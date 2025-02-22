using Microsoft.AspNetCore.Mvc;
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
}