using ExpenseManagement.Server.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ExpenseManagement.Server.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(IdentityResult, UserDto?)> RegisterAsync(RegisterDto registerDto, string role);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}