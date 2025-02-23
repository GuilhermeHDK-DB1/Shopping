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
    
    public async Task<IActionResult> ProductUpdate(long id)
    {
        var product = await _productService.FindByIdAsync(id);
        
        if (product != null)
            return View(product);
        
        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductModel product)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateAsync(product);

            if (result != null)
                return RedirectToAction(nameof(ProductIndex));
        }

        return View(product);
    }
    
    public async Task<IActionResult> ProductDelete(long id)
    {
        var product = await _productService.FindByIdAsync(id);
        
        if (product != null)
            return View(product);
        
        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductModel product)
    {
        var result = await _productService.DeleteAsync(product.Id);

        if (result)
            return RedirectToAction(nameof(ProductIndex));

        return View(product);
    }
}