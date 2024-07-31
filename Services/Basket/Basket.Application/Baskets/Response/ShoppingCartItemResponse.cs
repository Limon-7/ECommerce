namespace Basket.Application.Baskets.Response;

public class ShoppingCartItemResponse
{
    public ShoppingCartItemResponse(int quantity, string imageFile, decimal price, string productId, string productName)
    {
        Quantity = quantity;
        ImageFile = imageFile;
        Price = price;
        ProductId = productId;
        ProductName = productName;
    }

    public int Quantity { get; private set; }
    public string ImageFile { get; private set; }
    public decimal Price { get; private set; }
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
}