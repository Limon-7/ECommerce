using AutoMapper;
using Discount.Domain.Entities;
using Discount.Grpc.Protos;


namespace Discount.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
    }
}