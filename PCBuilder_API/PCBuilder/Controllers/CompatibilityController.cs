using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Dtos.PartsList;
using PCBuilder.Services.CompatibilityService;

namespace PCBuilder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompatibilityController : ControllerBase
    {
        private readonly ICompatibilityService _compatibilityService;

        public CompatibilityController(ICompatibilityService compatibilityService)
        {
            _compatibilityService = compatibilityService;
        }

        public CompatibilityService CompatibilityService
        {
            get => default;
            set
            {
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckCompatibility(PartListDto partList)
        {
            return Ok(await _compatibilityService.CheckCompatibility(partList));
        }
    }
}
