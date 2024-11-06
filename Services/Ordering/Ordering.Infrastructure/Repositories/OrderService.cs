using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderService : GenericRepository<Order>, IOrderService
{
    public OrderService(OrderDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName)
    {
        var orderList = await _dbContext.Orders
            .Where(o => o.UserName == userName)
            .ToListAsync();
        return orderList;
    }
}