using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly AccountRepository _accountRepository;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, AccountRepository accountRepository, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _accountRepository = accountRepository;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registrationData)
        {
            if (await _accountRepository.UserExists(registrationData.Username))
                return BadRequest("Username is taken");
            User user = await _accountRepository.Register(registrationData);
            return new UserDto
            {
                Username = registrationData.Username,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                User user = await _accountRepository.Login(loginDto);
                return new UserDto
                {
                    Username = loginDto.Username,
                    Token = _tokenService.CreateToken(user)
                }; 
            }
            catch (ArgumentException)
            {
                return BadRequest("Invalid username or password");
            }
        }
    }
}