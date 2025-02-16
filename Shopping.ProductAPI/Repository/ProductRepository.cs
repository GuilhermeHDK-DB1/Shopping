using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopping.ProductAPI.Data.ValueObjects;
using Shopping.ProductAPI.Model;
using Shopping.ProductAPI.Model.Context;

namespace Shopping.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public ProductRepository(
        Context context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ProductVO>> FindAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        
        return _mapper.Map<List<ProductVO>>(products);
    }

    public async Task<ProductVO?> FindByIdAsync(long id)
    {
        var product = await _context.Products
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> CreateAsync(ProductVO vo)
    {
        var product = _mapper.Map<Product>(vo);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO?> UpdateAsync(ProductVO vo)
    {
        var product = await _context.Products.FindAsync(vo.Id);
        if (product == null)
            return default;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (product == null)
                return false;
            
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}