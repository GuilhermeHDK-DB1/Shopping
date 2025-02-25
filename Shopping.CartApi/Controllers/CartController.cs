using Microsoft.AspNetCore.Mvc;
using Shopping.CartApi.Data.ValueObjects;
using Shopping.CartApi.Repository;

namespace Shopping.CartApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase
{
    [HttpGet("find-cart/{userId}")]
    public async Task<IActionResult> FindCartByUserId(
        string userId,
        [FromServices] ICartRepository repository)
    {
        var products = await repository.FindCartByUserIdAsync(userId);
        if (products is null) return NotFound();
        return Ok(products);
    }
    
    [HttpPost("add-cart")]
    public async Task<IActionResult> AddCart(
        [FromBody] CartVo vo,
        [FromServices] ICartRepository repository)
    {
        var products = await repository.SaveOrUpdateCartAsync(vo);
        if (products is null) return NotFound();
        return Ok(products);
    }
    
    [HttpPut("update-cart")]
    public async Task<IActionResult> UpdateCart(
        [FromBody] CartVo vo,
        [FromServices] ICartRepository repository)
    {
        var products = await repository.SaveOrUpdateCartAsync(vo);
        if (products is null) return NotFound();
        return Ok(products);
    }
    
    [HttpDelete("remove-cart/{cartDetailsId}")]
    public async Task<IActionResult> RemoveCart(
        long cartDetailsId,
        [FromServices] ICartRepository repository)
    {
        var status = await repository.RemoveFromCartAsync(cartDetailsId);
        if (!status) return NotFound();
        return Ok(status);
    }
}