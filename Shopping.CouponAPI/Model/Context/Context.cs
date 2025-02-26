using Microsoft.EntityFrameworkCore;

namespace Shopping.CouponAPI.Model.Context;

public class Context : DbContext
{
    public Context()
    { }

    public Context(DbContextOptions<Context> options) : base(options)
    { }
    
    public DbSet<Coupon> Coupons { get; set; }
    
}