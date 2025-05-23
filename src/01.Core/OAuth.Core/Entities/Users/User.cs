﻿using OAuth.Core.Entities.UserSites;

namespace OAuth.Core.Entities.Users;

public class User
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Mobile { get; set; }
    public required string UserName { get; set; }
    public required string HashPassword { get; set; }
    public DateTime? CreationDate { get; set; } = DateTime.UtcNow;

    public List<UserSite> UserSites { get; set; } = default!;
    public List<UserRole> UserRoles { get; set; } = default!;   
}
