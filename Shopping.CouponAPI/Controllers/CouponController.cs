using Microsoft.AspNetCore.Mvc;
using Shopping.CouponAPI.Repository.Interfaces;

namespace Shopping.CouponAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CouponController : ControllerBase
{
    [HttpGet("{couponCode}")]
    public async Task<IActionResult> Get(
        string couponCode,
        [FromServices] ICouponRepository repository)
    {
        var coupon = await repository.GetCouponByCodeAsync(couponCode);
        
        if(coupon == null)
            return NotFound();
        
        return Ok(coupon);
    }
}