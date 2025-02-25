using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopping.CartApi.Model.Base;

namespace Shopping.CartApi.Model;

[Table("cart_header")]
public class CartHeader : BaseEntity
{
    [Column("user_id")]
    public string UserId { get; set; }
    [Column("coupon_code")]
    public string? CouponCode { get; set; }
}