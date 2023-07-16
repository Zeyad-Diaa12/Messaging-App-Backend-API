using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApi.Data;
using SocialApi.DTOs;
using SocialApi.Interfaces;
using SocialApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace SocialApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext dataContext, ITokenService tokenService)
        {
            _dataContext = dataContext;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto userReg)
        {
            if (await UserExists(userReg.Username.ToLower()))
            {
                return BadRequest("User Exists");
            }

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = userReg.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userReg.Password)),
                PasswordSalt = hmac.Key
            };
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto userLog)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == userLog.Username.ToLower());
            if (user == null)
            {
                return Unauthorized("User Not Exists");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLog.Password));

            for(int i = 0;i<computedHash.Length;i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Wrong Password");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _dataContext.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
