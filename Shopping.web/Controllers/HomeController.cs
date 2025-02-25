using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.web.Models;
using Shopping.web.Services.IServices;

namespace Shopping.web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public HomeController(
        ILogger<HomeController> logger,
        IProductService productService,
        ICartService cartService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.FindAllAsync();
        return View(products);
    }
    
    [Authorize]
    public async Task<IActionResult> Details(long id)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var product = await _productService.FindByIdAsync(id, accessToken);
        return View(product);
    }
    
    [HttpPost]
    [ActionName("Details")]
    [Authorize]
    public async Task<IActionResult> DetailsPost(ProductViewModel product)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        CartViewModel cart = new()
        {
            CartHeader = new CartHeaderViewModel
            {
                UserId = User.Claims.Where(c => c.Type == "sub").FirstOrDefault()?.Value,
            }
        };

        CartDetailViewModel cartDetail = new()
        {
            Count = product.Count,
            ProductId = product.Id,
            Product = await _productService.FindByIdAsync(product.Id, accessToken)
        };
        
        var cartDetails = new List<CartDetailViewModel>();
        cartDetails.Add(cartDetail);
        cart.CartDetails = cartDetails;
        
        var response = await _cartService.AddItemToCartAsync(cart, accessToken);
        if (response != null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [Authorize]
    public async Task<IActionResult> Login()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}