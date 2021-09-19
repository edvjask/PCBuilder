using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Dtos.PartsList;
using PCBuilder.Dtos.Product;
using PCBuilder.Dtos.SpecificationDto;
using PCBuilder.Models;
using PCBuilder.Services.CompatibilityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICompatibilityService _compatibilityService;

        public ProductService(IMapper mapper, DataContext context, ICompatibilityService compatibilityService)
        {
            _context = context;
            _mapper = mapper;
            _compatibilityService = compatibilityService;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetSpecificProduct(ProductType type)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            List<Product> dbProcessors =
                await _context.Products
                .Where(p => p.ProductType == type)
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .ToListAsync();
            List<GetProductDto> productsDto = Maps.MapProductDto(dbProcessors);
            serviceResponse.Data = productsDto;
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            List<Product> dbProducts =
                await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .ToListAsync();
            List<GetProductDto> productsDto = Maps.MapProductDto(dbProducts);

            serviceResponse.Data = productsDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetForAmount(double amount)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();

            
            
            Product cpu =
            await _context.Products
            .Where(p => p.Id == 2)
            .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
            .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
            .FirstAsync();
            cpu.Adverts = cpu.Adverts.Where(a => a.Confirmed == true).ToList();

            Product cooler =
            await _context.Products
            .Where(p => p.Id == 6)
            .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
            .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
            .FirstAsync();
            cooler.Adverts = cooler.Adverts.Where(a => a.Confirmed == true).ToList();

            Product mobo =
            await _context.Products
            .Where(p => p.Id == 7)
            .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
            .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
            .FirstAsync();
            mobo.Adverts = mobo.Adverts.Where(a => a.Confirmed == true).ToList();

            Product ram =
            await _context.Products
            .Where(p => p.Id == 8)
            .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
            .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
            .FirstAsync();
            ram.Adverts = ram.Adverts.Where(a => a.Confirmed == true).ToList();

            Product storage =
            await _context.Products
            .Where(p => p.Id == 9)
            .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
            .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
            .FirstAsync();
            storage.Adverts = storage.Adverts.Where(a => a.Confirmed == true).ToList();

            Product gpu =
            await _context.Products
            .Where(p => p.Id == 10)
            .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
            .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
            .FirstAsync();
            gpu.Adverts = gpu.Adverts.Where(a => a.Confirmed == true).ToList();

            Product pcCase =
            await _context.Products
            .Where(p => p.Id == 11)
            .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
            .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
            .FirstAsync();
            pcCase.Adverts = pcCase.Adverts.Where(a => a.Confirmed == true).ToList();

            Product psu =
            await _context.Products
            .Where(p => p.Id == 13)
            .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
            .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
            .FirstAsync();
            psu.Adverts = psu.Adverts.Where(a => a.Confirmed == true).ToList();

            serviceResponse.Data = new List<GetProductDto> { 
                Maps.MapProductDto(cpu),
                Maps.MapProductDto(cooler),
                Maps.MapProductDto(mobo),
                Maps.MapProductDto(ram),
                Maps.MapProductDto(storage),
                Maps.MapProductDto(gpu),
                Maps.MapProductDto(pcCase),
                Maps.MapProductDto(psu)
            };






            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto addProduct)
        {
            ServiceResponse<GetProductDto> serviceResponse = new ServiceResponse<GetProductDto>();
            Product p = await _context.Products
                .Where(p => string.Compare(p.Name, addProduct.Name) == 0)
                .FirstOrDefaultAsync();
            if (p != null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Product already exists";
            }
            else
            {
                Product add = _mapper.Map<Product>(addProduct);
                await _context.Products.AddAsync(add);
                await _context.SaveChangesAsync();
                serviceResponse.Data = Maps.MapProductDto(add);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleCpu(PartListDto partList)
        {
            ServiceResponse<List<GetProductChoose>> serviceResponse = new ServiceResponse<List<GetProductChoose>>();
            List<Product> dbProducts =
                await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .Where(p => p.ProductType == ProductType.CPU)
                .ToListAsync();
            List<Product> compatibleCpu = new List<Product>();

            foreach(Product cpu in dbProducts)
            {
                    if ( _compatibilityService.CheckCpuMoboCompatibility(cpu.Id, partList.MotherboardId).Result.Count == 0
                        &&  _compatibilityService.CheckCpuRamCompatibility(cpu.Id, partList.MemoryId).Result.Count == 0
                        && _compatibilityService.CheckCpuWithCoolerCompatibility(cpu.Id, partList.CpuCoolerId).Result.Count == 0)
                    {

                    cpu.ProductSpecifications = cpu.ProductSpecifications.Where(ps =>
                        ps.Specification.Name.Equals("Core Count", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("Boost Clock", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("TDP", StringComparison.OrdinalIgnoreCase)
                        ).ToList();


                    compatibleCpu.Add(cpu);
                    }
                    
            }

            

            List<GetProductChoose> productsDto = Maps.MapProductChoose(compatibleCpu);

            serviceResponse.Data = productsDto;
            return serviceResponse;
        }


        public async Task<ServiceResponse<GetProductDto>> GetById(int id)
        {
            ServiceResponse<GetProductDto> serviceResponse = new ServiceResponse<GetProductDto>();
            Product p = await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            p.Adverts = p.Adverts.Where(a => a.Confirmed == true).ToList();
            if (p == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Product not found";
            }
            else
            {
                
                serviceResponse.Data = Maps.MapProductDto(p);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductFull>> GetFullById(int id)
        {
            ServiceResponse<GetProductFull> serviceResponse = new ServiceResponse<GetProductFull>();
            Product p = await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            if (p == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Product not found";
            }
            else
            {
                p.ProductSpecifications = p.ProductSpecifications.OrderBy(ps => ps.Specification.Name).ToList();
                serviceResponse.Data = Maps.MapProductFull(p);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleMobo(PartListDto partList)
        {
            ServiceResponse<List<GetProductChoose>> serviceResponse = new ServiceResponse<List<GetProductChoose>>();
            List<Product> dbProducts =
                await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .Where(p => p.ProductType == ProductType.Motherboard )
                .ToListAsync();
            List<Product> compatibleCpu = new List<Product>();

            foreach (Product mobo in dbProducts)
            {
                if (_compatibilityService.CheckCpuMoboCompatibility(partList.ProcessorId, mobo.Id).Result.Count == 0
                    && _compatibilityService.CheckGPUMoboCompatibility(partList.GpuId, mobo.Id).Result.Count == 0
                    && _compatibilityService.CheckMoboCaseCompatibility(mobo.Id, partList.CaseId).Result.Count == 0
                    && _compatibilityService.CheckRamMoboCompatibility(partList.MemoryId, mobo.Id).Result.Count == 0
                    && _compatibilityService.CheckStorageMoboCompatibility(mobo.Id, partList.StorageId).Result.Count == 0

                    )
                {

                    mobo.ProductSpecifications = mobo.ProductSpecifications.Where(ps =>
                        ps.Specification.Name.Equals("Form Factor", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("Socket", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("Max Memory", StringComparison.OrdinalIgnoreCase)
                        ).OrderBy(ps => ps.Specification.Name).ToList();


                    compatibleCpu.Add(mobo);
                }

            }



            List<GetProductChoose> productsDto = Maps.MapProductChoose(compatibleCpu);

            serviceResponse.Data = productsDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleStorage(PartListDto partList)
        {
            ServiceResponse<List<GetProductChoose>> serviceResponse = new ServiceResponse<List<GetProductChoose>>();
            List<Product> dbProducts =
                await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .Where(p => p.ProductType == ProductType.Storage)
                .ToListAsync();
            List<Product> compatible = new List<Product>();

            foreach (Product storage in dbProducts)
            {
                if (_compatibilityService.CheckStorageCaseCompatibility(storage.Id, partList.CaseId).Result.Count == 0
                    && _compatibilityService.CheckStorageMoboCompatibility(partList.MotherboardId, storage.Id).Result.Count == 0
                    

                    )
                {

                    storage.ProductSpecifications = storage.ProductSpecifications.Where(ps =>
                        ps.Specification.Name.Equals("Capacity", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("Interface", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("Form Factor", StringComparison.OrdinalIgnoreCase)
                        ).OrderBy(ps => ps.Specification.Name).ToList();

                    compatible.Add(storage);
                }

            }

            List<GetProductChoose> productsDto = Maps.MapProductChoose(compatible);

            serviceResponse.Data = productsDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleCpuCooler(PartListDto partList)
        {
            ServiceResponse<List<GetProductChoose>> serviceResponse = new ServiceResponse<List<GetProductChoose>>();
            List<Product> dbProducts =
                await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .Where(p => p.ProductType == ProductType.CPU_Cooler)
                .ToListAsync();
            List<Product> compatible = new List<Product>();

            //ignore components which are not necessary to check
            partList.GpuId = null;
            partList.MotherboardId = null;
            partList.MemoryId = null;
            partList.StorageId = null;
            partList.CaseId = null;
            partList.PSUId = null;


            foreach (Product cooler in dbProducts)
            {
                partList.CpuCoolerId = cooler.Id;
                if (_compatibilityService.CheckCompatibility(partList).Result.Data.Problems.Count == 0)
                {

                    cooler.ProductSpecifications = cooler.ProductSpecifications.Where(ps =>
                        ps.Specification.Name.Equals("Noise Level", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("Fan rpm", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("Type", StringComparison.OrdinalIgnoreCase)
                        ).OrderBy(ps => ps.Specification.Name).ToList();

                    compatible.Add(cooler);
                }

            }

            List<GetProductChoose> productsDto = Maps.MapProductChoose(compatible);

            serviceResponse.Data = productsDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleRam(PartListDto partList)
        {
            ServiceResponse<List<GetProductChoose>> serviceResponse = new ServiceResponse<List<GetProductChoose>>();
            List<Product> dbProducts =
                await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .Where(p => p.ProductType == ProductType.Memory)
                .ToListAsync();
            List<Product> compatible = new List<Product>();

            //ignore components which are not necessary to check
            partList.GpuId = null;
            partList.CpuCoolerId = null;
            partList.CaseId = null;
            partList.PSUId = null;


            foreach (Product ram in dbProducts)
            {
                partList.MemoryId = ram.Id;
                if (_compatibilityService.CheckCompatibility(partList).Result.Data.Problems.Count == 0)
                {

                    ram.ProductSpecifications = ram.ProductSpecifications.Where(ps =>
                        ps.Specification.Name.Equals("speed", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("type", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("module size", StringComparison.OrdinalIgnoreCase)
                        ).OrderBy(ps => ps.Specification.Name).ToList();

                    compatible.Add(ram);
                }

            }

            List<GetProductChoose> productsDto = Maps.MapProductChoose(compatible);

            serviceResponse.Data = productsDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleGpu(PartListDto partList)
        {
            ServiceResponse<List<GetProductChoose>> serviceResponse = new ServiceResponse<List<GetProductChoose>>();
            List<Product> dbProducts =
                await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .Where(p => p.ProductType == ProductType.Video_Card)
                .ToListAsync();
            List<Product> compatible = new List<Product>();

            //ignore components which are not necessary to check
            partList.ProcessorId = null;
            partList.CpuCoolerId = null;
            partList.MemoryId = null;
            partList.StorageId = null;



            foreach (Product p in dbProducts)
            {
                partList.GpuId = p.Id;
                if (_compatibilityService.CheckCompatibility(partList).Result.Data.Problems.Count == 0)
                {

                    p.ProductSpecifications = p.ProductSpecifications.Where(ps =>
                        ps.Specification.Name.Equals("boost clock", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("memory", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("Length", StringComparison.OrdinalIgnoreCase)
                        ).OrderBy(ps => ps.Specification.Name).ToList();

                    compatible.Add(p);
                }

            }

            List<GetProductChoose> productsDto = Maps.MapProductChoose(compatible);

            serviceResponse.Data = productsDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductChoose>>> GetCompatibleCase(PartListDto partList)
        {
            ServiceResponse<List<GetProductChoose>> serviceResponse = new ServiceResponse<List<GetProductChoose>>();
            List<Product> dbProducts =
                await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .Where(p => p.ProductType == ProductType.Case)
                .ToListAsync();
            List<Product> compatible = new List<Product>();

            //ignore components which are not necessary to check

            partList.MemoryId = null;
            partList.ProcessorId = null;
            partList.CpuCoolerId = null;



            foreach (Product p in dbProducts)
            {
                partList.CaseId = p.Id;
                if (_compatibilityService.CheckCompatibility(partList).Result.Data.Problems.Count == 0)
                {

                    p.ProductSpecifications = p.ProductSpecifications.Where(ps =>
                        ps.Specification.Name.Equals("Type", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("GPU Length", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("INTERNAL 2.5 BAYS", StringComparison.OrdinalIgnoreCase)
                        ).OrderBy(ps => ps.Specification.Name).ToList();

                    compatible.Add(p);
                }

            }

            List<GetProductChoose> productsDto = Maps.MapProductChoose(compatible);

            serviceResponse.Data = productsDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductChoose>>> GetCompatiblePSU(PartListDto partList)
        {
            ServiceResponse<List<GetProductChoose>> serviceResponse = new ServiceResponse<List<GetProductChoose>>();
            List<Product> dbProducts =
                await _context.Products
                .Include(p => p.ProductSpecifications).ThenInclude(ps => ps.Specification)
                .Include(p => p.Adverts).ThenInclude(advs => advs.Seller)
                .Where(p => p.ProductType == ProductType.Power_Supply)
                .ToListAsync();
            List<Product> compatible = new List<Product>();

            //ignore components which are not necessary to check
            partList.ProcessorId = null;
            partList.CpuCoolerId = null;
            partList.MotherboardId = null;
            partList.MemoryId = null;
            partList.StorageId = null;

            foreach (Product p in dbProducts)
            {
                partList.PSUId = p.Id;
                if (_compatibilityService.CheckCompatibility(partList).Result.Data.Problems.Count == 0)
                {

                    p.ProductSpecifications = p.ProductSpecifications.Where(ps =>
                        ps.Specification.Name.Equals("form factor", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("sata connectors", StringComparison.OrdinalIgnoreCase) ||
                        ps.Specification.Name.Equals("wattage", StringComparison.OrdinalIgnoreCase)
                        ).OrderBy(ps => ps.Specification.Name).ToList();

                    compatible.Add(p);
                }

            }

            List<GetProductChoose> productsDto = Maps.MapProductChoose(compatible);

            serviceResponse.Data = productsDto;
            return serviceResponse;
        }

        
    }
}
