using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Dtos.Advert;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PCBuilder.Services.AdvertService
{
    public class AdvertService : IAdvertService
    {

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AdvertService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetAdvertDto>> AddAdvert(AddAdvertDto newAdvert)
        {
            ServiceResponse<GetAdvertDto> response = new ServiceResponse<GetAdvertDto>();
            try
            {
                Product p = await _context.Products
                .Where(p => p.Id == newAdvert.ProductId)
                .FirstOrDefaultAsync();
                if (p == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Product not found";
                    return response;
                }

                bool priceGood = CheckPrice(newAdvert.Price);
                if (!priceGood)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Invalid price";
                    return response;
                }
                bool descGood = CheckDesc(newAdvert.Description);
                if (!descGood)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Please enter a detailed description";
                    return response;
                }

                User s = await _context.Users.
                    Where(s => s.Id == GetUserId())
                    .FirstOrDefaultAsync();

                Advert a = new Advert
                {
                    Confirmed = false,
                    CreatedOn = DateTime.Now,
                    Description = newAdvert.Description,
                    LastEditedOn = DateTime.Now,
                    Price = newAdvert.Price,
                    Used = newAdvert.Used,
                    Product = p,
                    Seller = s

                };

                await _context.Adverts.AddAsync(a);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetAdvertDto>(a);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
            

            
        }

        public async Task<ServiceResponse<List<GetAdvertDto>>> GetUserListings()
        {
            ServiceResponse<List<GetAdvertDto>> response = new ServiceResponse<List<GetAdvertDto>>();
            try
            {
                List<Advert> dbAdverts = await _context.Adverts
                    .Include(a => a.Seller)
                    .Include(a => a.Product)
                    .Where(a => a.Seller.Id == GetUserId())
                    .ToListAsync();



                response.Data = (dbAdverts.Select(a => _mapper.Map<GetAdvertDto>(a))).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        
        public async Task<ServiceResponse<List<GetAdvertDto>>> GetAdverts()
        {
            ServiceResponse<List<GetAdvertDto>> response = new ServiceResponse<List<GetAdvertDto>>();
            try
            {
                List<Advert> dbAdverts = await _context.Adverts
                    .Include(a => a.Seller)
                    .Include(a => a.Product)
                    .Where(a => a.Confirmed == true)
                    .ToListAsync();



                response.Data = (dbAdverts.Select(a => _mapper.Map<GetAdvertDto>(a))).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        
        public async Task<ServiceResponse<List<GetAdvertDto>>> GetAdverts(int productId)
        {
            ServiceResponse<List<GetAdvertDto>> response = new ServiceResponse<List<GetAdvertDto>>();
            try
            {
                List<Advert> dbAdverts = await _context.Adverts
                    .Include(a => a.Seller)
                    .Include(a => a.Product)
                    .Where(a => a.Product.Id == productId && a.Confirmed == true)
                    .ToListAsync();


                response.Data = (dbAdverts.Select(a => _mapper.Map<GetAdvertDto>(a))).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<GetAdvertDto>> GetById(int id)
        {
            ServiceResponse<GetAdvertDto> response = new ServiceResponse<GetAdvertDto>();
            try
            {
                Advert a = await _context.Adverts
                    .Include(a => a.Seller)
                    .Include(a => a.Product)
                    .Where(a => a.Id == id && a.Confirmed == true)
                    .FirstOrDefaultAsync();
                if (a == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Advert doesn't exist or hasn't been approved yet.";
                    return response;
                }

                response.Data = _mapper.Map<GetAdvertDto>(a);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetAdvertDto>>> GetUnconfirmedAdverts()
        {
            ServiceResponse<List<GetAdvertDto>> response = new ServiceResponse<List<GetAdvertDto>>();
            try
            {
                List<Advert> dbAdverts = await _context.Adverts
                    .Include(a => a.Seller)
                    .Include(a => a.Product)
                    .Where(a => a.Confirmed == false)
                    .ToListAsync();


                response.Data = (dbAdverts.Select(a => _mapper.Map<GetAdvertDto>(a))).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetAdvertDto>> ConfirmAdvert(int id)
        {
            ServiceResponse<GetAdvertDto> response = new ServiceResponse<GetAdvertDto>();
            try
            {
                Advert a = await _context.Adverts
                    .Include(a => a.Seller)
                    .Include(a => a.Product)
                    .Where(a => a.Id == id)
                    .FirstOrDefaultAsync();
                if (a == null || a.Confirmed)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Advert doesn't exist or is already confirmed.";
                    return response;
                }
                a.Confirmed = true;
                _context.Adverts.Update(a);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetAdvertDto>(a);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetAdvertDto>>> RemoveAdvert(int id)
        {
            ServiceResponse<List<GetAdvertDto>> response = new ServiceResponse<List<GetAdvertDto>>();
            try
            {
                Advert a = GetUserRole().Equals("Admin") ?
                    await _context.Adverts.Where(a => a.Id == id).FirstOrDefaultAsync() :
                    await _context.Adverts
                    .Include(a => a.Seller)
                    .Where(a => a.Id == id && a.Seller.Id == GetUserId()).FirstOrDefaultAsync();

                if (a != null)
                {
                    _context.Adverts.Remove(a);
                    await _context.SaveChangesAsync();
                    response.Message = "Advert deleted successfully.";
                }
                else
                {
                    response.Message = "Advert not found";
                    response.Success = false;
                }


                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetAdvertDto>> UpdateAdvert(UpdateAdvertDto newAdvert, int id)
        {
            ServiceResponse<GetAdvertDto> response = new ServiceResponse<GetAdvertDto>();
            try
            {
                Advert a = GetUserRole().Equals("Admin") ?
                    await _context.Adverts
                    .Include(a => a.Seller)
                    .Include(a => a.Product)
                    .Where(a => a.Id == id)
                    .FirstOrDefaultAsync() :
                    await _context.Adverts
                    .Include(a => a.Seller)
                    .Include(a => a.Product)
                    .Where(a => a.Id == id && a.Seller.Id == GetUserId())
                    .FirstOrDefaultAsync();

                if (a != null)
                {
                    bool changes = false;
                    if (newAdvert.Price != null)
                    {
                        if (!CheckPrice((double)newAdvert.Price)) {
                            response.Success = false;
                            response.Message = "Updated price is invalid.";
                            return response;
                        }
                        changes = true;
                        a.Price = (double)newAdvert.Price;
                       
                    }

                    if (newAdvert.Description != null)
                    {
                        if (!CheckDesc(newAdvert.Description))
                        {
                            response.Success = false;
                            response.Message = "Updated description is invalid.";
                            return response;
                        }
                        changes = true;
                        a.Description = newAdvert.Description;
                        
                    }

                    if (newAdvert.Used != null)
                    {
                        a.Used = (bool) newAdvert.Used;
                        changes = true;
                    }

                    if (newAdvert.ProductId != null)
                    {
                        Product p = await _context.Products.Where(p => p.Id == newAdvert.ProductId).FirstOrDefaultAsync();
                        if (p == null)
                        {
                            response.Success = false;
                            response.Message = "Updated product is not found.";
                            return response;
                        }
                        changes = true;
                        a.Product = p;
                        
                    }
                    if (changes)
                    {
                        a.LastEditedOn = DateTime.Now;
                        _context.Adverts.Update(a);
                        await _context.SaveChangesAsync();
                    }
                        


                    
                    response.Data = _mapper.Map<GetAdvertDto>(a);

                }
                else
                {
                    response.Success = false;
                    response.Message = "Advert not found";
                }
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }


        //Helper methods
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        private bool CheckPrice(double price)
        {
            if (price <= 0)
            {
                return false;
            }
            return true;
        }
        private bool CheckDesc(string desc)
        {
            if (desc == null || desc.Length <= 10)
            {
                return false;
            }return true;
        }
        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

    }
}
