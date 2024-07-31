using Basket.Domain.Entities;

namespace Basket.Application.Common.Interfaces;

public interface IBasketService
{
    Task<ShoppingCart> GetBasket(string userName);
    Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart);
    Task DeleteBasket(string userName);
}