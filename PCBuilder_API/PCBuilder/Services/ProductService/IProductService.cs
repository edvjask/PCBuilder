using PCBuilder.Dtos.PartsList;
using PCBuilder.Dtos.Product;
using PCBuilder.Dtos.SpecificationDto;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDto>>> GetSpecificProduct(ProductType type);
        Task<ServiceResponse<List<GetProductDto>>> GetAllProducts();

        Task<ServiceResponse<GetProductDto>> GetById(int id);

        Task<ServiceResponse<List<GetProductDto>>> GetForAmount(double amount);

        Task<ServiceResponse<GetProductFull>> GetFullById(int id);
        Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto addProduct);

        Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleCpu(PartListDto partList);
        Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleMobo(PartListDto partList);
        Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleStorage(PartListDto partList);
        Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleCpuCooler(PartListDto partList);
        Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleRam(PartListDto partList);
        Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleGpu(PartListDto partList);
        Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleCase(PartListDto partList);

        Task<ServiceResponse<List<GetProductChoose>>> GetCompatiblePSU(PartListDto partList);
    }
}
