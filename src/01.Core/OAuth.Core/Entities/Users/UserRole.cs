namespace OAuth.Core.Entities.Users;

public class UserRole
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public required string UserId { get; set; }

    public Role Role { get; set; } = default!;
    public User User { get; set; } = default!;
}
