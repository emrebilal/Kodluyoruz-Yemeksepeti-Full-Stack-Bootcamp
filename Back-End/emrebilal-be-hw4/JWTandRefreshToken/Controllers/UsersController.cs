using JWTandRefreshToken.Data;
using JWTandRefreshToken.Models;
using JWTandRefreshToken.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTandRefreshToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly ApiContext _context;
        readonly IConfiguration _configuration;
        public UsersController(ApiContext content, IConfiguration configuration)
        {
            _context = content;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public async Task<bool> Create([FromForm] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        [HttpPost("[action]")]
        public async Task<TokenModel> Login([FromForm] UserLogin userLogin)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userLogin.UserName && x.Password == userLogin.Password);
            if (user != null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                TokenModel token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(3);
                await _context.SaveChangesAsync();

                return token;
            }
            return null;
        }

        [HttpGet("[action]")]
        public async Task<TokenModel> RefreshTokenLogin([FromForm] string refreshToken)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.Now)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                TokenModel token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(3);
                await _context.SaveChangesAsync();

                return token;
            }
            return null;
        }
    }
}
