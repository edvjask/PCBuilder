using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Data;
using PCBuilder.Models;
using PCBuilder.Services.AdvertService;
using PCBuilder.Services.ImageUploadService;
using Microsoft.AspNetCore.Authorization;

namespace PCBuilder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IImageUploadService _imageUploadService;

        public ImageUploadController(IImageUploadService imageUploadService)
        {
            _imageUploadService = imageUploadService;
        }

        [HttpPost("post/{id}")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> OnAdvertUploadAsync(List<IFormFile> files, int id)
        {
            return Ok(await _imageUploadService.AdvertPhotosUpload(files, id));
            
        }
        [HttpPost("product/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnAdvertUploadAsync(IFormFile file, int id)
        {
            return Ok(await _imageUploadService.AddProductPhoto(file, id));

        }
    }
}
