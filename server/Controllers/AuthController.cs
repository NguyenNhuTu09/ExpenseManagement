using ExpenseManagement.Server.Common;
using ExpenseManagement.Server.Dtos;
using ExpenseManagement.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenseManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (result, userDto) = await _authService.RegisterAsync(registerDto, AppRole.User);

            if (!result.Succeeded || userDto == null)
            {
                return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
            }
            return Ok(new { Message = "Đăng ký thành công. Vui lòng đăng nhập.", UserId = userDto.Id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authResponse = await _authService.LoginAsync(loginDto);

            if (authResponse == null)
                return Unauthorized(new { Message = "Tên đăng nhập hoặc mật khẩu không chính xác." });

            return Ok(authResponse);
        }
    }
}
