using OAuth.Core.Entities.Users;

public class Role
{
    public long Id { get; set; }
    public required string RoleName { get; set; }

    public List<UserRole> UserRoles { get; set; } = default!;
}