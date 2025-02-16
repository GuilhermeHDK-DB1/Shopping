using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopping.ProductAPI.Model.Base;

namespace Shopping.ProductAPI.Model;

[Table("product")]
public class Product : BaseEntity
{
    [Column("name")]
    [Required]
    [StringLength(150)]
    public String Name { get; set; }
    
    [Column("price")]
    [Required]
    [Range(1, 10000)]
    public Decimal Price { get; set; }
    
    [Column("description")]
    [StringLength(500)]
    public String Description { get; set; }
    
    [Column("category_name")]
    [StringLength(50)]
    public String CategoryName { get; set; }
    
    [Column("image_url")]
    [StringLength(300)]
    public String ImageUrl { get; set; }
}