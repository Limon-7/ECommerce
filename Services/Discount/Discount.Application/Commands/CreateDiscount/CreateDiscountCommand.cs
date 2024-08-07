using AutoMapper;
using Discount.Domain.Entities;
using Discount.Domain.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands.CreateDiscount;

public class CreateDiscountCommand: IRequest<CouponModel>
{
    public CreateDiscountCommand(string productId, string productName, string description, int amount)
    {
        ProductId = productId;
        ProductName = productName;
        Description = description;
        Amount = amount;
    }

    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string Description { get; private set; }
    public int Amount { get; private set; }
}

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }
    public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = _mapper.Map<Coupon>(request);
        await _discountRepository.CreateDiscount(coupon);
        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
}