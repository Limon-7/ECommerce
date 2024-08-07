using Discount.Domain.Repositories;
using MediatR;

namespace Discount.Application.Commands.DeleteDiscountByProductId;

public class DeleteDiscountByProductId : IRequest<bool>
{
    public string ProductId { get; set; }

    public DeleteDiscountByProductId(string productId)
    {
        ProductId = productId;
    }
}

public class DeleteDiscountByProductIdCommandHandler : IRequestHandler<DeleteDiscountByProductId, bool>
{
    private readonly IDiscountRepository _discountRepository;

    public DeleteDiscountByProductIdCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<bool> Handle(DeleteDiscountByProductId request, CancellationToken cancellationToken)
    {
        var deleted = await _discountRepository.DeleteDiscountByProductId(request.ProductId);
        return deleted;
    }
}