using Microsoft.IdentityModel.Tokens;
using OAuth.Application.JwtTokenService;
using OAuth.Application.JwtTokenService.Dtos;
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

    public async Task<string> GenerateToken(UserInfoForJwtDto dto)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, dto.UserId),
            new Claim(ClaimTypes.Name, Guid.NewGuid().ToString()),
            new Claim("Site", dto.SiteAudience),
            new Claim("Phone_Number",dto.Mobile),
            new Claim("FirstName",dto.FirstName),
            new Claim("LastName",dto.LastName)
        };
        if (dto.UserRole != null)
        {
            foreach (var role in dto.UserRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        var key = _configuration["jwt:key"];
        var issuer = _configuration["jwt:issuer"];
        var audience = _configuration["jwt:Audience"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddHours(30),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var result = tokenHandler.WriteToken(securityToken);
        return await Task.FromResult(result);
    }
}
