using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Dtos.Advert;
using PCBuilder.Services.AdvertService;

namespace PCBuilder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService _advertService;

        public AdvertController(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> CreateAdvert(AddAdvertDto addAdvert)
        {
            return Ok(await _advertService.AddAdvert(addAdvert));
        }

        [HttpGet("user")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> GetUserListings()
        {
            return Ok(await _advertService.GetUserListings());
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAdverts()
        {
            return Ok(await _advertService.GetAdverts());
        }
        [HttpGet("all/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAdverts(int productId)
        {
            return Ok(await _advertService.GetAdverts(productId));
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _advertService.GetById(id));
        }
        [HttpGet("unconfirmed")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUnconfirmed()
        {
            return Ok(await _advertService.GetUnconfirmedAdverts());
        }
        [HttpPut("confirm/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmAdvert(int id)
        {
            return Ok(await _advertService.ConfirmAdvert(id));
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> RemoveAdvert(int id)
        {
            return Ok(await _advertService.RemoveAdvert(id));
        }
        [HttpPut("edit/{id}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> EditAdvert(UpdateAdvertDto updateAdvert, int id)
        {
            return Ok(await _advertService.UpdateAdvert(updateAdvert, id));
        }
    }
}
