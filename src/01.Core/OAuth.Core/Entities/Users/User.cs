using OAuth.Core.Entities.UserSites;
using System.Net.Http.Headers;

namespace OAuth.Core.Entities.Users;

public class User
{
    public required string Id { get; set; }
    public  string? Name { get; set; }
    public  string? LastName { get; set; }
    public  string? Mobile { get; set; }
    public  string? UserName { get; set; }
    public  string? HashPassword { get; set; }
    public DateTime? CreationDate { get; set; } = DateTime.UtcNow;
    public string? Email { get; set; }
    public List<UserSite> UserSites { get; set; } = default!;
    public List<UserRole> UserRoles { get; set; } = default!;   
}
