using Microsoft.IdentityModel.Tokens;
using OAuth.Application.JwtTokenService;
using OAuth.Application.JwtTokenService.Dtos;
using OAuth.Core.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuth.Presentation.ImplementationServices;

public class JwtTokenService : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> GenerateToken(UserInfoForJwtDto dto)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, dto.UserId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("site", dto.SiteAudience),
            new Claim("Phone_Number",dto.Mobile)
        };
        if (dto.UserRole != null)
        {
            foreach(var role in dto.UserRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));   
            }
        }

        var key = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = dto.SiteAudience; // یا مقدار audience دلخواه

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult(tokenString);
    }
}
