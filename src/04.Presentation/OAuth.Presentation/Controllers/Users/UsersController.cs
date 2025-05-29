using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OAuth.Application.Services.Users.Contracts;
using OAuth.Application.Services.Users.Contracts.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuth.Presentation.Controllers.Users;


[ApiController]
[Route("api/users")]
public class UsersController : Controller
{

    private readonly IUserService _service;
    private readonly IConfiguration _configuration;
    public UsersController(IUserService service,
        IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<string> Add(AddUserDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpGet("all")]
    [Authorize(Roles="Administrator")]
    public async Task<List<GetAllUsersDto>> GetAll()
    {
        return await _service.GetAll();
    }

    [HttpGet("check")]
    public async Task<string> CheckToken()
    {

        var key = _configuration["jwt:key"];
        var issuer = _configuration["jwt:issuer"];
        var audience = _configuration["jwt:Audience"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.NameIdentifier, "gjhkv"),
                     new Claim(ClaimTypes.Name, "Reza"),
                     new Claim(ClaimTypes.Role,"admin")
                     // Add additional claims as needed (e.g., roles)
                 };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = issuer,
            Expires = DateTime.UtcNow.AddDays(1),
            Audience = audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
        };


        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var result = tokenHandler.WriteToken(securityToken);
        return await Task.FromResult(result);


    }
}
