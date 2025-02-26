namespace Shopping.web.Models;

public class CouponViewModel
{
    public long Id { get; set; }
    public String CouponCode { get; set; }
    public Decimal DiscountAmount { get; set; }
}