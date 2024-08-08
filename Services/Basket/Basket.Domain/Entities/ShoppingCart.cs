namespace Basket.Domain.Entities;

public class ShoppingCart
{
    public string UserName { get;  set; } = null!;
    public List<ShoppingCartItem> Items { get;  set; } = null!;

    public ShoppingCart()
    {
    }

    public ShoppingCart(string userName, List<ShoppingCartItem> items)
    {
        UserName = userName;
        Items = items;
    }
}