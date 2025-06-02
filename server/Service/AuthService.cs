using AutoMapper;
using ExpenseManagement.Server.Data;
using ExpenseManagement.Server.Dtos;
using ExpenseManagement.Server.Entities;
using ExpenseManagement.Server.Helpers;
using ExpenseManagement.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<(IdentityResult, UserDto?)> RegisterAsync(RegisterDto registerDto, string roleName)
        {
            var userExists = await _userManager.FindByNameAsync(registerDto.Username);
            if (userExists != null)
                return (IdentityResult.Failed(new IdentityError { Description = "Tên đăng nhập đã tồn tại." }), null);

            userExists = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userExists != null)
                return (IdentityResult.Failed(new IdentityError { Description = "Email đã được sử dụng." }), null);

            var user = _mapper.Map<ApplicationUser>(registerDto);
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return (result, null);

            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            
            await _userManager.AddToRoleAsync(user, roleName);

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = new List<string> { roleName };
            return (result, userDto);
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            ApplicationUser? user = null;
            if (loginDto.LoginIdentifier.Contains("@")) {
                user = await _userManager.FindByEmailAsync(loginDto.LoginIdentifier);
            } else {
                user = await _userManager.FindByNameAsync(loginDto.LoginIdentifier);
            }

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return null;

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>() 
                ?? throw new InvalidOperationException("JWT settings are not configured.");
            
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = (List<string>)userRoles;

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                User = userDto
            };
        }
    }
}