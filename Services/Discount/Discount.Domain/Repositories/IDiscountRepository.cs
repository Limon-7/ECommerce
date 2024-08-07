using Discount.Domain.Entities;

namespace Discount.Domain.Repositories;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscountByProductId(string productName);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscountByProductId(string productId);
}