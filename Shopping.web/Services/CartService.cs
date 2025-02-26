using System.Net.Http.Headers;
using Shopping.web.Models;
using Shopping.web.Services.IServices;
using Shopping.web.Utils;

namespace Shopping.web.Services;

public class CartService : ICartService
{
    private readonly HttpClient _httpClient;
    public const string BasePath = "api/v1/cart";

    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<CartViewModel?> FindCartByUserIdAsync(string userId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.GetAsync($"{BasePath}/find-cart/{userId}");
        
        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            return null;
        
        return await response.ReadContentAs<CartViewModel?>();
    }

    public async Task<CartViewModel> AddItemToCartAsync(CartViewModel cart, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.PostAsJson($"{BasePath}/add-cart", cart);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong creating the product");
        
        return await response.ReadContentAs<CartViewModel>();
    }

    public async Task<CartViewModel> UpdateCartAsync(CartViewModel cart, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.PutAsJson($"{BasePath}/update-cart", cart);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong creating the product");
        
        return await response.ReadContentAs<CartViewModel>();
    }

    public async Task<bool> RemoveCartAsync(long cartId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.DeleteAsync($"{BasePath}/remove-cart/{cartId}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong deleting the product");
        
        return await response.ReadContentAs<bool>();
    }

    public async Task<bool> ApplyCouponAsync(CartViewModel cart, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.PostAsJson($"{BasePath}/apply-coupon", cart);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong applying the coupon");
        
        return await response.ReadContentAs<bool>();
    }

    public async Task<bool> RemoveCouponAsync(string userId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.DeleteAsync($"{BasePath}/remove-coupon/{userId}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong deleting the coupon");
        
        return await response.ReadContentAs<bool>();
    }

    public Task<bool> ClearCart(string userId, string token)
    {
        throw new NotImplementedException();
    }

    public Task<CartViewModel> CheckoutAsync(CartHeaderViewModel cartHeader, string token)
    {
        throw new NotImplementedException();
    }
}