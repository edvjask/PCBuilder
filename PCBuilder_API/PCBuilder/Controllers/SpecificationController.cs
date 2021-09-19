using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Dtos.Specification;
using PCBuilder.Models;
using PCBuilder.Services.SpecificationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecificationController : ControllerBase
    {
        private readonly ISpecificationService _specificationService;

        public SpecificationController(ISpecificationService specificationService)
        {
            _specificationService = specificationService;
        }

        public SpecificationService SpecificationService
        {
            get => default;
            set
            {
            }
        }

        public async Task<IActionResult> GetAllSpecs()
        {
            return Ok(await _specificationService.GetAllSpecs());
        }
        [HttpGet("{type}")]
        public async Task<IActionResult> GetSpecificSpecs(ProductType type)
        {
            return Ok(await _specificationService.GetSpecificSpecs(type));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSpec(AddSpecificationDto addSpecification)
        {
            return Ok(await _specificationService.AddSpec(addSpecification));
        }
    }
}
