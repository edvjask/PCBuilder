using Microsoft.AspNetCore.Mvc;
using PCBuilder.Dtos.PartsList;
using PCBuilder.Dtos.Product;
using PCBuilder.Models;
using PCBuilder.Services.CompatibilityService;
using PCBuilder.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        

        public ProductsController(IProductService productService)
        {
            _productService = productService;
           
        }

        public ProductService ProductService
        {
            get => default;
            set
            {
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.GetAllProducts());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ServiceResponse<GetProductDto> response =  await _productService.GetById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("full/{id}")]
        public async Task<IActionResult> GetFullById(int id)
        {
            ServiceResponse<GetProductFull> response = await _productService.GetFullById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("all/{type}")]

        public async Task<IActionResult> GetAllByType(ProductType type)
        {
            ServiceResponse<List<GetProductDto>> response = await _productService.GetSpecificProduct(type);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpPost("getcomplete")]

        public async Task<IActionResult> GetForAmount(double amount)
        {
            ServiceResponse<List<GetProductDto>> response = await _productService.GetForAmount(amount);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddProductDto add)
        {
            return Ok(await _productService.AddProduct(add));
        }
        [HttpPost("compatible_cpu")]
        public async Task<IActionResult> GetCompatibleCpu(PartListDto partList)
        {
            return Ok(await _productService.GetCompatibleCpu(partList));
        }
        [HttpPost("compatible_mobo")]
        public async Task<IActionResult> GetCompatibleMobo(PartListDto partList)
        {
            return Ok(await _productService.GetCompatibleMobo(partList));
        }
        [HttpPost("compatible_storage")]
        public async Task<IActionResult> GetCompatibleStorage(PartListDto partList)
        {
            return Ok(await _productService.GetCompatibleStorage(partList));
        }
        [HttpPost("compatible_cpucooler")]
        public async Task<IActionResult> GetCompatibleCpuCooler(PartListDto partList)
        {
            return Ok(await _productService.GetCompatibleCpuCooler(partList));
        }
        [HttpPost("compatible_ram")]
        public async Task<IActionResult> GetCompatibleRam(PartListDto partList)
        {
            return Ok(await _productService.GetCompatibleRam(partList));
        }
        [HttpPost("compatible_gpu")]
        public async Task<IActionResult> GetCompatibleGpu(PartListDto partList)
        {
            return Ok(await _productService.GetCompatibleGpu(partList));
        }
        [HttpPost("compatible_case")]
        public async Task<IActionResult> GetCompatibleCase(PartListDto partList)
        {
            return Ok(await _productService.GetCompatibleCase(partList));
        }
        [HttpPost("compatible_psu")]
        public async Task<IActionResult> GetCompatiblePsu(PartListDto partList)
        {
            return Ok(await _productService.GetCompatiblePSU(partList));
        }
    }
}
