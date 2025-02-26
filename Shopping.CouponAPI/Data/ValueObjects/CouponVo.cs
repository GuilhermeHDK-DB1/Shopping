namespace Shopping.CouponAPI.Data.ValueObjects;

public class CouponVo
{
    public long Id { get; set; }
    public String CouponCode { get; set; }
    public Decimal DiscountAmount { get; set; }
}