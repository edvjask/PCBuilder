using PCBuilder.Dtos.Product;
using PCBuilder.Dtos.SpecificationDto;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Data
{
    public class Maps
    {
        public static List<GetProductDto> MapProductDto(List<Product> dbProducts)
        {
            List<GetProductDto> productsDto = new List<GetProductDto>();

            foreach (Product p in dbProducts)
            {
                if (p.Adverts != null && p.Adverts.Count > 0)
                {
                    Advert lowestPrice = new Advert();
                    double minPrice = 1000000;

                    foreach (Advert a in p.Adverts)
                    {
                        if (a.Price.CompareTo(minPrice) < 0)
                        {
                            minPrice = a.Price;
                            lowestPrice = a;
                        }
                    }


                    productsDto.Add(new GetProductDto
                    {
                        Id = p.Id,
                        ImagePath = p.ImagePath,
                        Location = lowestPrice == null ? "-" : lowestPrice.Seller.Location,
                        Name = p.Name,
                        LowestPrice = lowestPrice == null ? -1 : lowestPrice.Price,
                        Phone = lowestPrice.Seller.Phone,
                        ProductType = p.ProductType
                    }) ;
                }
                else
                {
                    productsDto.Add(new GetProductDto
                    {
                        Id = p.Id,
                        ImagePath = p.ImagePath,
                        Name = p.Name,
                        ProductType = p.ProductType
                    });
                }
                
            }

            return productsDto;

        }
        public static GetProductDto MapProductDto(Product p)
        {
            if (p.Adverts != null && p.Adverts.Count > 0)
            {
                Advert lowestPrice = new Advert();
                double minPrice = 1000000;

                foreach (Advert a in p.Adverts)
                {
                    if (a.Price.CompareTo(minPrice) < 0)
                    {
                        minPrice = a.Price;
                        lowestPrice = a;
                    }
                }

                return new GetProductDto
                {
                    Id = p.Id,
                    ImagePath = p.ImagePath,
                    Location = lowestPrice == null ? "-" : lowestPrice.Seller.Location,
                    Name = p.Name,
                    LowestPrice = lowestPrice == null ? -1 : lowestPrice.Price,
                    Phone = lowestPrice.Seller.Phone,
                    ProductType = p.ProductType
                };
            }
            else
            {
                return new GetProductDto
                {
                    Id = p.Id,
                    ImagePath = p.ImagePath,
                    Name = p.Name,
                    ProductType = p.ProductType
                };
            }
            
        }

        public static GetProductFull MapProductFull (Product p)
        {
            Dictionary<string, GetMultipleSpecsDto> specs = new Dictionary<string, GetMultipleSpecsDto>();

            foreach (ProductSpecifications ps in p.ProductSpecifications)
            {
                if (specs.TryGetValue(ps.Specification.Name, out var value))
                {
                    value.Value.Add(ps.Value);
                }
                else
                {
                    specs.Add(ps.Specification.Name, new GetMultipleSpecsDto {
                        Name = ps.Specification.Name,
                        Value = new List<string>{ ps.Value }
                    });
                }
            }

            GetProductFull dto = new GetProductFull
            {
                Id = p.Id,
                Name = p.Name,
                ProductType = p.ProductType,
                Specifications = specs.Select(s => s.Value).ToList(),
                ImagePath = p.ImagePath
            };


            return dto;
        }

        public static List<GetProductChoose> MapProductChoose(List<Product> products)
        {
            List<GetProductChoose> newList = new List<GetProductChoose>();
            foreach(Product p in products)
            {
                List<GetSpecificationDto> specs = new List<GetSpecificationDto>();

                double lowestPrice = -1;
                if (p.Adverts != null && p.Adverts.Count > 0)
                {
                    
                    double minPrice = 1000000;

                    foreach (Advert a in p.Adverts)
                    {
                        if (a.Price.CompareTo(minPrice) < 0)
                        {
                            minPrice = a.Price;
                        }
                    }
                    lowestPrice = minPrice;
                }

                foreach (ProductSpecifications ps in p.ProductSpecifications)
                {
                    specs.Add(new GetSpecificationDto
                    {
                        Name = ps.Specification.Name,
                        Value = ps.Value
                    });
                }
                newList.Add( new GetProductChoose
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProductType = p.ProductType,
                    Specifications = specs,
                    ImagePath = p.ImagePath,
                    LowestPrice = lowestPrice
                });
            }
            
            return newList;
        }


        //public static GetProductBasicDto MapProductBasicDto(Product p)
        //{
        //    Advert lowestPrice = new Advert();
        //    double minPrice = 1000000;

        //    foreach(Advert a in p.Adverts)
        //    {
        //        if (a.Price.CompareTo(minPrice) < 0)
        //        {
        //            minPrice = a.Price;
        //            lowestPrice = a;
        //        }
        //    }

        //    return new GetProductBasicDto {
        //        Id = p.Id,
        //        ImagePath = p.ImagePath,
        //        Location = lowestPrice.Seller.Location,
        //        Name = p.Name,
        //        LowestPrice = lowestPrice.Price,
        //        ProductType = p.ProductType
        //    };

        //}
    }
}
