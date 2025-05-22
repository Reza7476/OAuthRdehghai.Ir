using System.Reflection.PortableExecutable;

namespace OAuth.Application.JwtTokenService.Dtos;

public class UserInfoForJwtDto
{
    public string Mobile  { get; set; }
    public string UserId { get; set; }
    public List<string> UserRole { get; set; }
    public string SiteAudience { get; internal set; }
}
