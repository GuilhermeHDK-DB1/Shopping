using Microsoft.EntityFrameworkCore;

namespace Shopping.OrderAPI.Model.Context;

public class Context : DbContext
{
    public Context()
    { }

    public Context(DbContextOptions<Context> options) : base(options)
    { }
    
    public DbSet<OrderDetail> Details { get; set; }
    public DbSet<OrderHeader> Headers { get; set; }
}