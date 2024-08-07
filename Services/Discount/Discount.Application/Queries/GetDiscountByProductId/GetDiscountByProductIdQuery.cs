using AutoMapper;
using Discount.Domain.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Queries.GetDiscountByProductId;

public class GetDiscountByProductIdQuery: IRequest<CouponModel>
{
    public string ProductId { get; private set; }

    public GetDiscountByProductIdQuery(string productId)
    {
        ProductId = productId;
    }
}

public class GetDiscountByProductIdQueryHandler : IRequestHandler<GetDiscountByProductIdQuery, CouponModel>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetDiscountByProductIdQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }
    public async Task<CouponModel> Handle(GetDiscountByProductIdQuery request, CancellationToken cancellationToken)
    {
        var coupon = await _discountRepository.GetDiscountByProductId(request.ProductId);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount with the product name = {request.ProductId} not found"));
        }
        //TODO: Exercise Follow Product Mapper kind of example
        var couponModel = new CouponModel
        {
            Id = coupon.Id,
            Amount = coupon.Amount,
            Description = coupon.Description,
            ProductName = coupon.ProductName
        };
        return couponModel;
    }
}