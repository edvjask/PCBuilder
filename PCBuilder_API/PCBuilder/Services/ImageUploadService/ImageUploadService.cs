using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PCBuilder.Data;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PCBuilder.Services.ImageUploadService
{
    public interface IImageUploadService
    {
        Task<ServiceResponse<string>> AdvertPhotosUpload(List<IFormFile> files, int id);
        Task<ServiceResponse<string>> AddProductPhoto(IFormFile file, int id);
    }


    public class ImageUploadService : IImageUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageUploadService(IWebHostEnvironment webHostEnvironment, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<string>> AddProductPhoto(IFormFile file, int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            Product p = await _context.Products
                .Where(p => p.Id == id )
                .FirstOrDefaultAsync();

            if (p != null)
            {
                var filePath = _webHostEnvironment.WebRootPath + "\\images\\products\\";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (file.Length > 0)
                {
                    string ext = file.FileName.Split('.').Last();
                    string fileName = $"{id}.{ext}";
                    using (var stream = System.IO.File.Create(filePath + fileName))
                    {
                        await file.CopyToAsync(stream);
                    }
                    p.ImagePath = "https://localhost:5001/images/products/" + fileName;
                    _context.Products.Update(p);
                    await _context.SaveChangesAsync();


                    response.Data = "Product image added successfully.";
                }

            }
            else
            {
                response.Success = false;
                response.Message = "Product not found.";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> AdvertPhotosUpload(List<IFormFile> files, int id)
        {

            ServiceResponse<string> response = new ServiceResponse<string>();

            if (files.Count > 4)
            {
                response.Success = false;
                response.Message = "Please select no more than 4 photos.";
            }

            Advert a = await _context.Adverts
                .Include(a => a.Seller)
                .Where(a => a.Id == id && a.Seller.Id.Equals(GetUserId()))
                .FirstOrDefaultAsync();

            if (a != null)
            {
                long size = files.Sum(f => f.Length);
                var filePath = _webHostEnvironment.WebRootPath + "\\images\\adverts\\" + $"\\{id}\\";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                //if photos already exist
                List<AdvertPhotos> ap = await _context.AdvertPhotos.Include(a => a.Advert).Where(ap => ap.Advert.Id == id).ToListAsync();
                if (ap != null)
                {
                    _context.AdvertPhotos.RemoveRange(ap);
                    await _context.SaveChangesAsync();
                }

                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].Length > 0)
                    {
                        string ext = files[i].FileName.Split('.').Last();
                        string fileName = $"{i + 1}.{ext}";
                        using (var stream = System.IO.File.Create(filePath + fileName))
                        {
                            await files[i].CopyToAsync(stream);
                        }
                        
                        
                        await _context.AdvertPhotos.AddAsync(new AdvertPhotos
                        {
                            Path = "https://localhost:5001/images/adverts/" + id + "/" + fileName,
                            Advert = a
                        });
                        
                        
                    }
                }
                await _context.SaveChangesAsync();

                response.Data = $"{files.Count} images uploaded successfully";

                
            }
            else
            {
                response.Success = false;
                response.Message = "Advert not found";
            }
            return response;
        }


        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
    }
}
