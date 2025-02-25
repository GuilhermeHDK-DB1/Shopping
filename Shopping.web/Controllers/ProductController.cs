using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.web.Models;
using Shopping.web.Services.IServices;
using Shopping.web.Utils;

namespace Shopping.web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [Authorize]
    public async Task<IActionResult> ProductIndex()
    {
        var products = await _productService.FindAllAsync();
        return View(products);
    }
    
    public ViewResult ProductCreate()
    {
        return View();
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductViewModel productView)
    {
        if (ModelState.IsValid)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var result = await _productService.CreateAsync(productView, accessToken);

            if (result != null)
                return RedirectToAction(nameof(ProductIndex));
        }

        return View(productView);
    }
    
    public async Task<IActionResult> ProductUpdate(long id)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var product = await _productService.FindByIdAsync(id, accessToken);
        
        if (product != null)
            return View(product);
        
        return NotFound();
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductViewModel productView)
    {
        if (ModelState.IsValid)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var result = await _productService.UpdateAsync(productView, accessToken);

            if (result != null)
                return RedirectToAction(nameof(ProductIndex));
        }

        return View(productView);
    }
    
    [Authorize]
    public async Task<IActionResult> ProductDelete(long id)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var product = await _productService.FindByIdAsync(id, accessToken);
        
        if (product != null)
            return View(product);
        
        return NotFound();
    }
    
    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> ProductDelete(ProductViewModel productView)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var result = await _productService.DeleteAsync(productView.Id, accessToken);

        if (result)
            return RedirectToAction(nameof(ProductIndex));

        return View(productView);
    }
}