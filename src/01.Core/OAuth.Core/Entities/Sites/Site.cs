using OAuth.Core.Entities.UserSites;

namespace OAuth.Core.Entities.Sites;

public class Site
{
    public long Id { get; set; }
    public required string SiteName { get; set; }
    public required string SiteUrl { get; set; }

    public HashSet<UserSite> UserSites { get; set; } = default!;
}

