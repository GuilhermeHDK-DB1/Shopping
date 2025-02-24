using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopping.CartApi.Model.Base;

namespace Shopping.CartApi.Model;

[Table("product")]
public class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id")]
    public long Id { get; set; }
    
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