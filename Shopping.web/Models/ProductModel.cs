namespace Shopping.web.Models;

public class ProductModel
{
    public long Id { get; set; }
    public String Name { get; set; }
    public Decimal Price { get; set; }
    public String Description { get; set; }
    public String CategoryName { get; set; }
    public String ImageUrl { get; set; }
}