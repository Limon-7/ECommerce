using Ordering.Domain.Common;

namespace Ordering.Application.Common.Interfaces;

public interface IOrderService : IGenericRepository<Domain.Entities.Order>
{
    Task<IEnumerable<Domain.Entities.Order>> GetOrdersByUserNameAsync(string userName);
}