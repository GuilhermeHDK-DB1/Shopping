using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.web.Models;
using Shopping.web.Services.IServices;

namespace Shopping.web.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;

    public CartController(
        IProductService productService,
        ICartService cartService,
        ICouponService couponService)
    {
        _productService = productService;
        _cartService = cartService;
        _couponService = couponService;
    }
    
    [Authorize]
    public async Task<IActionResult> CartIndex()
    {
        return View(await FindUserCart());
    }

    public async Task<IActionResult> Remove(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

        var response = await _cartService.RemoveCartAsync(id, token);

        if(response)
        {
            return RedirectToAction(nameof(CartIndex));
        }
        return View();
    }
                
    private async Task<CartViewModel?> FindUserCart()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

        var response = await _cartService.FindCartByUserIdAsync(userId, token);

        if (response?.CartHeader != null)
        {
            if (!string.IsNullOrEmpty(response.CartHeader.CouponCode))
            {
                var coupon = await _couponService.
                    GetCouponAsync(response.CartHeader.CouponCode, token);
                if (coupon?.CouponCode != null)
                {
                    response.CartHeader.DiscountAmount = coupon.DiscountAmount;
                }
            }

            response.CartHeader.PurchaseAmount = 0;
            foreach (var detail in response.CartDetails)
            {
                response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
            }
            response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountAmount;
        }
        return response;
    }
    
    [HttpPost]
    [ActionName("ApplyCoupon")]
    public async Task<IActionResult> ApplyCoupon(CartViewModel cart)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

        var response = await _cartService.ApplyCouponAsync(cart, token);

        if(response)
        {
            return RedirectToAction(nameof(CartIndex));
        }
        return View();
    }
    
    [HttpPost]
    [ActionName("RemoveCoupon")]
    public async Task<IActionResult> RemoveCoupon()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

        var response = await _cartService.RemoveCouponAsync(userId, token);

        if(response)
        {
            return RedirectToAction(nameof(CartIndex));
        }
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> CheckoutIndex()
    {
        return View(await FindUserCart());
    }
}