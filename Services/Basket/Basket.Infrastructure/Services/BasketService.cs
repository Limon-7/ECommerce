using Basket.Application.Common.Interfaces;
using Basket.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Services;

public class BasketService: IBasketService
{
    private readonly IDistributedCache _redisCache;

    public BasketService(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }
    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
        {
            return null!;
        }

        return JsonConvert.DeserializeObject<ShoppingCart>(basket)!;
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
    {
        await _redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
        var response= await GetBasket(shoppingCart.UserName);
        return response;
    }

    public async Task DeleteBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }

}