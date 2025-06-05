using OAuth.Common.Interfaces;
using OAuth.Core.Entities.Sites;
using OAuth.Core.Entities.UserSites;

namespace OAuth.Application.Services.Sites.Contarcts;

public interface ISiteRepository : IRepository
{
    Task Add(Site site);
    Task AddUserSite(UserSite userSite);
    Task<bool> checkUserSite(long id, string userId);
    Task<Site?> GetSiteBySiteName(string siteAudience);
    Task<long?> GetSiteIdBySiteUrl(string url);
    Task<bool> IsExist(string siteUrl);
    Task<bool> IsUserBelongToSite(string userId, long? siteId);
}