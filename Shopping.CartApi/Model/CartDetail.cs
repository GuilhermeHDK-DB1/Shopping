using System.ComponentModel.DataAnnotations.Schema;
using Shopping.CartApi.Model.Base;

namespace Shopping.CartApi.Model;

[Table("cart_detail")]
public class CartDetail : BaseEntity
{
    [ForeignKey("CartHeaderId")]
    public long CartHeaderId { get; set; }
    public CartHeader CartHeader { get; set; }
    
    [ForeignKey("ProductId")]
    public long ProductId { get; set; }
    public Product Product { get; set; }
    
    [Column("count")]
    public int Count { get; set; }
}