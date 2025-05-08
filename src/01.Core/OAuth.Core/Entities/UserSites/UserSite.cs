using OAuth.Core.Entities.Sites;
using OAuth.Core.Entities.Users;

namespace OAuth.Core.Entities.UserSites;

public class UserSite
{
    public long Id { get; set; }
    public long SiteId { get; set; }
    public required string UserId { get; set; }

    public User User { get; set; } = default!;
    public Site Site { get; set; } = default!;
}
