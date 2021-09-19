using PCBuilder.Dtos.Advert;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Services.AdvertService
{
    public interface IAdvertService
    {
        Task<ServiceResponse<List<GetAdvertDto>>> GetAdverts();

        Task<ServiceResponse<List<GetAdvertDto>>> GetUserListings();

        Task<ServiceResponse<List<GetAdvertDto>>> GetAdverts(int productId);

        Task<ServiceResponse<GetAdvertDto>> GetById(int id);

        Task<ServiceResponse<List<GetAdvertDto>>> GetUnconfirmedAdverts();

        Task<ServiceResponse<GetAdvertDto>> AddAdvert(AddAdvertDto newAdvert);
        Task<ServiceResponse<GetAdvertDto>> UpdateAdvert(UpdateAdvertDto newAdvert, int id);

        Task<ServiceResponse<List<GetAdvertDto>>> RemoveAdvert(int id);

        Task<ServiceResponse<GetAdvertDto>> ConfirmAdvert(int id);
    }
}
