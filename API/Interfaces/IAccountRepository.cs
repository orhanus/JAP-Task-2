using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> Register(RegisterDto registrationData);
        Task<bool> UserExists(string username);
        Task<User> Login(LoginDto loginData);
        Task<User> GetUserByUsername(string username);
    }
}