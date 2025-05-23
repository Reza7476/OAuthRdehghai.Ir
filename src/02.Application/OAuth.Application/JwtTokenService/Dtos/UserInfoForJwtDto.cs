using System.Reflection.PortableExecutable;

namespace OAuth.Application.JwtTokenService.Dtos;

public class UserInfoForJwtDto
{
    public required string Mobile  { get; set; }
    public required string UserId { get; set; }
    public List<string> UserRole { get; set; } = default!;
    public required string SiteAudience { get;  set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
