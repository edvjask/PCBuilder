using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Dtos.Product;
using PCBuilder.Dtos.ProductSpecification;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Services.ProductSpecificationService
{
    public class ProductSpecificationService : IProductSpecificationService
    {
        private readonly DataContext _context;

        public ProductSpecificationService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<GetProductDto>> AddProductSpec(AddProductSpecificationDto addProductSpecification)
        {
            ServiceResponse<GetProductDto> response = new ServiceResponse<GetProductDto>();
            try
            {
                ProductSpecifications ps = await _context.ProductSpecifications
                    .FirstOrDefaultAsync(ps => ps.ProductId == addProductSpecification.ProductId &&
                            ps.SpecificationId == addProductSpecification.SpecificationId);
                if (ps != null)
                {
                    response.Success = false;
                    response.Message = "This specification for this product already exists";
                    return response;
                }

                Product product = await _context.Products
                    .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                    .FirstOrDefaultAsync(p => p.Id == addProductSpecification.ProductId);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found";
                    return response;
                }
                Specification spec = await _context.Specifications
                    .FirstOrDefaultAsync(s => s.Id == addProductSpecification.SpecificationId);
                if (spec == null)
                {
                    response.Success = false;
                    response.Message = "Specification not found";
                    return response;
                }
                if (spec.ProductType != product.ProductType)
                {
                    response.Success = false;
                    response.Message = "Specification and product don't match";
                    return response;
                }
                ProductSpecifications productSpecifications = new ProductSpecifications
                {
                    Product = product,
                    Specification = spec,
                    Value = addProductSpecification.Value
                };

                await _context.ProductSpecifications.AddAsync(productSpecifications);
                await _context.SaveChangesAsync();

                response.Data = Maps.MapProductDto(product);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
