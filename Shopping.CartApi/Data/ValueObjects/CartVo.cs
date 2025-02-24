namespace Shopping.CartApi.Data.ValueObjects;

public class CartVo
{
    public CartHeaderVo CartHeader { get; set; }
    public IEnumerable<CartDetailVo> CartDetails { get; set; }
}