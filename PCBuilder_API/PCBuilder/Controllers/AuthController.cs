using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Data;
using PCBuilder.Dtos.Seller;
using PCBuilder.Models;

namespace PCBuilder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;


        public AuthController(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegister request)
        {
            ServiceResponse<int> response = await _authRepository.Register(
                new User { Name = request.Name, Email = request.Email,
                    Location = request.Location, Phone = request.Phone }, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin request)
        {
            ServiceResponse<GetLoggedUser> response = await _authRepository.Login(
                request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
