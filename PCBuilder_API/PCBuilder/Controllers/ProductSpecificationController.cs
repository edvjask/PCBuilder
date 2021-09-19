using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Dtos.ProductSpecification;
using PCBuilder.Services.ProductSpecificationService;

namespace PCBuilder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductSpecificationController : ControllerBase
    {
        private readonly IProductSpecificationService _productSpecificationService;

        public ProductSpecificationController(IProductSpecificationService productSpecificationService)
        {
            _productSpecificationService = productSpecificationService;
        }


        [HttpPost]
        public async Task<IActionResult> addProductSpec(AddProductSpecificationDto add)
        {
            return Ok(await _productSpecificationService.AddProductSpec(add));
        }
    }
}
