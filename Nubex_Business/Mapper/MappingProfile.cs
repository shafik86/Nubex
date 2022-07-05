using AutoMapper;
using Nubex_DataAccess;
using Nubex_Models;


namespace Nubex_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductPremium, ProductPremiumDTO>().ReverseMap();
            //CreateMap<OrderHeaderDTO, OrderHeader>().ReverseMap();
            //CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            //CreateMap<OrderDTO, Order>().ReverseMap();
        }
    }
}
