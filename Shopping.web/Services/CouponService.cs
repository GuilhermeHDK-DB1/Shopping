using System.Net.Http.Headers;
using Shopping.web.Models;
using Shopping.web.Services.IServices;
using Shopping.web.Utils;

namespace Shopping.web.Services;

public class CouponService : ICouponService
{
    private readonly HttpClient _httpClient;
    public const string BasePath = "api/v1/coupon";

    public CouponService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CouponViewModel> GetCouponAsync(string couponCode, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.GetAsync($"{BasePath}/{couponCode}");
        
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return new CouponViewModel();
        
        return await response.ReadContentAs<CouponViewModel>();
    }
}