using Microsoft.EntityFrameworkCore;
using OAuth.Application.Services.Sites.Contarcts;
using OAuth.Core.Entities.Sites;
using OAuth.Core.Entities.UserSites;

namespace OAuth.Infrastructure.Repositories.Sites;

public class EFSiteRepository : ISiteRepository
{
    private readonly DbSet<Site> _sites;
    private readonly DbSet<UserSite> _userSite;
    
    public EFSiteRepository(EFDataContext context)
    {
        _sites = context.Set<Site>();
        _userSite = context.Set<UserSite>();
    }

    public async Task<bool> checkUserSite(long id, string userId)
    {
        return await _userSite.AnyAsync(_ => _.SiteId == id && _.UserId == userId);
    }

    public async Task<Site?> GetSiteBySiteName(string siteAudience)
    {
        return await _sites
              .Where(_ => _.SiteUrl == siteAudience)
              .FirstOrDefaultAsync();
    }
}
