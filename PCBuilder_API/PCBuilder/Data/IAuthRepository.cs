using PCBuilder.Dtos.Seller;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<GetLoggedUser>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}
