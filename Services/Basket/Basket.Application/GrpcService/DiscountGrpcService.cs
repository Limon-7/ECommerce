using Discount.Grpc.Protos;

namespace Basket.Application.GrpcService;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    {
        _discountProtoServiceClient = discountProtoServiceClient;
    }
    
    public async Task<CouponModel> GetDiscount(string productId)
    {
        var discountRequest = new GetDiscountRequest {ProductId = productId};
        var response = await _discountProtoServiceClient.GetDiscountByProductIdAsync(discountRequest);
        return response;
    }
}