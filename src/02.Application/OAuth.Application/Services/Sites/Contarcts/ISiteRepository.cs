using OAuth.Common.Interfaces;
using OAuth.Core.Entities.Sites;
using OAuth.Core.Entities.UserSites;

namespace OAuth.Application.Services.Sites.Contarcts;

public interface ISiteRepository : IRepository
{
    Task<bool> checkUserSite(long id, string userId);
    Task<Site?> GetSiteBySiteName(string siteAudience);
}
