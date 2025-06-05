using System.Reflection.PortableExecutable;

namespace OAuth.Application.JwtTokenService.Dtos;

public class UserInfoForJwtDto
{
    public  string? Mobile  { get; set; }
    public  required string UserId { get; set; }
    public List<string> UserRole { get; set; } = default!;
    public  required string SiteAudience { get;  set; }
    public  string? FirstName { get; set; }
    public  string? LastName { get; set; }
    public string? Email { get; set; }
}
