namespace Basket.Domain.Entities;

public class ShoppingCartItem
{
    public ShoppingCartItem(int quantity, decimal price, string productId, string imageFile, string productName)
    {
        Quantity = quantity;
        Price = price;
        ProductId = productId;
        ImageFile = imageFile;
        ProductName = productName;
    }

    public int Quantity { get; private set; }
    public decimal Price { get; set; }
    public string ProductId { get; private set; }
    public string ImageFile { get; private set; }
    public string ProductName { get; private set; }
}