using Microsoft.EntityFrameworkCore;

namespace Shopping.ProductAPI.Model.Context;

public class Context : DbContext
{
    public Context()
    { }

    public Context(DbContextOptions<Context> options) : base(options)
    { }
    
    public DbSet<Product> Products { get; set; }
}