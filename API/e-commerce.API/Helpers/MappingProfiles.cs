using AutoMapper;
using Core.Entities;
using e_commerce.API.Dtos;

namespace e_commerce.API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(d => d.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(d => d.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}
