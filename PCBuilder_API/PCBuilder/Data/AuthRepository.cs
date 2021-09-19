using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using PCBuilder.Data;
using PCBuilder.Models;
using PCBuilder.Dtos.Seller;
using System.Text.RegularExpressions;

namespace PCBuilder.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<ServiceResponse<GetLoggedUser>> Login(string email, string password)
        {

            

            ServiceResponse<GetLoggedUser> response = new ServiceResponse<GetLoggedUser>();

            if (email == null || password == null)
            {
                response.Success = false;
                response.Message = "Invalid email or password format";
                return response;
            }

            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null || 
                !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Invalid email or password.";

            }
            else
            {
                response.Data = new GetLoggedUser {
                    Name = user.Name,
                    Token = CreateToken(user),
                    Role = user.Role.ToLower()
                };
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();

            //check if all fields populated
            if (user.Name == null ||
                user.Email == null ||
                user.Phone == null ||
                user.Location == null ||
                password == null)
            {
                response.Success = false;
                response.Message = "All fields must be populated.";
                return response;
            }


            //check for valid email
            if (!IsValidEmail(user.Email))
            {
                response.Success = false;
                response.Message = "Email not in correct format.";
                return response;
            }
            //check for valid phone number
            //if (!IsPhoneNumber(user.Phone))
            //{
            //    response.Success = false;
            //    response.Message = "Phone not in correct format.";
            //    return response;
            //}


            if (await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "User with this email already exists.";
                return response;
            }


            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passWordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passWordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^\+? (\d[\d -. ] +)?(\([\d -. ] +\))?[\d-. ]+\d$").Success;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.
                UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
