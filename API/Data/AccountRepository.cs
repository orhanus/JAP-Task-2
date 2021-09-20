using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Register(RegisterDto registrationData) 
        {
            using var hmac = new HMACSHA512();

            User user = new User{
                Username = registrationData.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationData.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<bool> UserExists(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
            return user != null;
        }
        public async Task<User> Login(LoginDto loginData)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginData.Username);
            if (user == null) throw new ArgumentException("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginData.Password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != user.PasswordHash[i]) throw new ArgumentException("Invalid password");

            return user;
        }
    }
}