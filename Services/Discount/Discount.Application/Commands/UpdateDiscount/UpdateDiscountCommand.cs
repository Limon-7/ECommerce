using AutoMapper;
using Discount.Domain.Entities;
using Discount.Domain.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands.UpdateDiscount;

public class UpdateDiscountCommand : IRequest<CouponModel>
{
    public UpdateDiscountCommand(int id, string productId, string productName, string description, int amount)

    {
        Id = id;
        ProductId = productId;
        ProductName = productName;
        Description = description;
        Amount = amount;
    }

    public int Id { get; private set; }
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string Description { get; private set; }
    public int Amount { get; private set; }
}

public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = _mapper.Map<Coupon>(request);
        await _discountRepository.UpdateDiscount(coupon);
        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
}