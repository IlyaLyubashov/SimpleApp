using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeCMS.Controllers.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FakeCMS.DAL;
using Microsoft.AspNetCore.Identity;
using FakeCMS.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FakeCMS.BL.Models.Account;

namespace FakeCMS.Api
{

    public class AccountController : BaseApiController
    {
        private readonly DbContextFakeCms _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(DbContextFakeCms dbContext, UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
            if (!loginResult.Succeeded)
                return BadRequest("Неправильно введен логин или пароль.");

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.Username);
            var token = GenerateJwtToken(user);
            return Ok(new { token = token });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = new User
            {
                UserName = createUserDto.Username
            };
            var createResult = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!createResult.Succeeded)
                return new BadRequestObjectResult(createResult.Errors);
            return Ok();
        }


        private object GenerateJwtToken( User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
            };

            var jwtConfig = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                //issuer: jwtConfig["Issuer"],
                //audience: jwtConfig["Audience"],
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}