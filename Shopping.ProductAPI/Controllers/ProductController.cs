using Microsoft.AspNetCore.Mvc;
using Shopping.ProductAPI.Data.ValueObjects;
using Shopping.ProductAPI.Repository;

namespace Shopping.ProductAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet()]
    public async Task<IActionResult> FindAll(
        [FromServices] IProductRepository repository)
    {
        var products = await repository.FindAllAsync();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(
        long id,
        [FromServices] IProductRepository repository)
    {
        var product = await repository.FindByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ProductVO vo,
        [FromServices] IProductRepository repository)
    {
        var product = await repository.CreateAsync(vo);
        return Ok(product);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] ProductVO vo,
        [FromServices] IProductRepository repository)
    {
        var product = await repository.UpdateAsync(vo);
        return Ok(product);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        long id,
        [FromServices] IProductRepository repository)
    {
        var success = await repository.DeleteAsync(id);
        if (!success) return BadRequest();
        return Ok(success);
    }
}