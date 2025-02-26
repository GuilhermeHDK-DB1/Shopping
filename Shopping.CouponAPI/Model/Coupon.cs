using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopping.CouponAPI.Model.Base;

namespace Shopping.CouponAPI.Model;

[Table("coupon")]
public class Coupon : BaseEntity
{
    [Column("coupon_code")]
    [Required]
    [StringLength(30)]
    public String CouponCode { get; set; }
    
    [Column("discount_amount")]
    [Required]
    public Decimal DiscountAmount { get; set; }
}