using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrsCleanCapstone.Controllers.API.Interfaces;
using MrsCleanCapstone.Data;
using MrsCleanCapstone.DTOs;
using MrsCleanCapstone.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Controllers.API
{

    public class AccountController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(ApplicationDbContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<EmployeeDto>> Login(LoginDto loginDto)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if (employee == null)
            {
                return Unauthorized("Invalid UserName!!");
            }

            using var hmac = new HMACSHA512(employee.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != employee.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new EmployeeDto
            {
                UserName = employee.UserName,
                Token = _tokenService.CreateToken(employee)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<EmployeeDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.UserName))
            {
                return BadRequest("UserName is taken");
            }

            using var hmac = new HMACSHA512();

            var employee = new Employee()
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return new EmployeeDto
            {
                UserName = employee.UserName,
                Token = _tokenService.CreateToken(employee)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Employees.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
