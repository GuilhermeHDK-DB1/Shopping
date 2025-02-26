using Microsoft.EntityFrameworkCore;

namespace Shopping.CouponAPI.Model.Context;

public class Context : DbContext
{
    public Context()
    { }

    public Context(DbContextOptions<Context> options) : base(options)
    { }
    
    public DbSet<Coupon> Coupons { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            Id = 1,
            CouponCode = "DESCONTO_10",
            DiscountAmount = 10
        });
        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            Id = 2,
            CouponCode = "DESCONTO_15",
            DiscountAmount = 15
        });
    }
    
}