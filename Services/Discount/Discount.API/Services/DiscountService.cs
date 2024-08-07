using Discount.Application.Commands.CreateDiscount;
using Discount.Application.Commands.DeleteDiscountByProductId;
using Discount.Application.Commands.UpdateDiscount;
using Discount.Application.Queries.GetDiscountByProductId;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(IMediator mediator, ILogger<DiscountService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public override async Task<CouponModel> GetDiscountByProductId(GetDiscountRequest request,
        ServerCallContext context)
    {
        var query = new GetDiscountByProductIdQuery(request.ProductId);
        var result = await _mediator.Send(query);
        _logger.LogInformation(
            $"Discount is retrieved for the Product Name: {request.ProductId} and Amount : {result.Amount}");
        return result;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var cmd = new CreateDiscountCommand(request.Coupon.ProductId, request.Coupon.ProductName,
            request.Coupon.Description, request.Coupon.Amount);
        var result = await _mediator.Send(cmd);
        _logger.LogInformation($"Discount is successfully created for the Product Name: {result.ProductName}");
        return result;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var cmd = new UpdateDiscountCommand(request.Coupon.Id, request.Coupon.ProductId, request.Coupon.ProductName,
            request.Coupon.Description, request.Coupon.Amount);
        var result = await _mediator.Send(cmd);
        _logger.LogInformation($"Discount is successfully updated Product Id: {result.ProductId}");
        return result;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscountByProductId(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var cmd = new DeleteDiscountByProductId(request.ProductId);
        var deleted = await _mediator.Send(cmd);
        var response = new DeleteDiscountResponse
        {
            Success = deleted
        };
        return response;
    }
}