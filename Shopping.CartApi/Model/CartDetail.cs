using System.ComponentModel.DataAnnotations.Schema;
using Shopping.CartApi.Model.Base;

namespace Shopping.CartApi.Model;

[Table("cart_detail")]
public class CartDetail : BaseEntity
{
    public long CartHeaderId { get; set; }
    [ForeignKey("CartHeaderId")]
    public CartHeader CartHeader { get; set; }
    
    public long ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    
    [Column("count")]
    public int Count { get; set; }
}