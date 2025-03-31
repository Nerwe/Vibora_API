using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vibora_API.Models.DTO;
using Vibora_API.Services;

namespace Vibora_API.Auth
{
    public class JwtProvider(IOptions<JwtOptions> options, IUsersService usersService) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;
        private readonly IUsersService _usersService = usersService;

        public async Task<string> GenerateToken(UserDTO userDTO)
        {
            var claims = new List<Claim>
                    {
                        new(CustomClaims.UserID, userDTO.ID.ToString())
                    };

            var permissions = await _usersService.GetUserPermissionsAsync(userDTO.ID);
            var roles = await _usersService.GetUserRolesAsync(userDTO.ID);

            foreach (var role in roles)
            {
                claims.Add(new(CustomClaims.Roles, role));
            }

            foreach (var permission in permissions)
            {
                claims.Add(new(CustomClaims.Permissions, permission));
            }

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpireHours));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
