namespace Discount.Domain.Entities;

public class Coupon
{
    public int Id { get; private set; }

    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string Description { get; private set; }
    public int Amount { get; private set; }

    public Coupon(string productId, string productName, string description, int amount, int id = default)
    {
        Id = id;
        ProductId = productId;
        ProductName = productName;
        Description = description;
        Amount = amount;
    }
}