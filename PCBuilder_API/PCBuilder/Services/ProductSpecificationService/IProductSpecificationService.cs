using PCBuilder.Dtos.Product;
using PCBuilder.Dtos.ProductSpecification;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Services.ProductSpecificationService
{
    public interface IProductSpecificationService
    {
        Task<ServiceResponse<GetProductDto>> AddProductSpec(AddProductSpecificationDto addProductSpecification);
    }
}
