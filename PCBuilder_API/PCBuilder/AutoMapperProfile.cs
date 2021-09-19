using AutoMapper;
using PCBuilder.Dtos.Advert;
using PCBuilder.Dtos.Product;
using PCBuilder.Dtos.Seller;
using PCBuilder.Dtos.Specification;
using PCBuilder.Dtos.SpecificationDto;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductSpecifications, GetSpecificationDto>()
                .ForMember(p => p.Name, p => p.MapFrom(p => p.Specification.Name));
            CreateMap<Product, GetProductDto>();
            CreateMap<Specification, GetSpecification>();
            CreateMap<AddSpecificationDto, Specification>();
            CreateMap<AddProductDto, Product>();
            CreateMap<User, GetUserDto>();
            CreateMap<Advert, GetAdvertDto>()
                .ForMember(a => a.ProductName, a => a.MapFrom(a => a.Product.Name));
            
                
        }
        
    }
}
