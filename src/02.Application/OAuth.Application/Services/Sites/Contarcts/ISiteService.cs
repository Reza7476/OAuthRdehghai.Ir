using OAuth.Application.Services.Sites.Contarcts.Dtos;
using OAuth.Common.Interfaces;

namespace OAuth.Application.Services.Sites.Contarcts;

public interface ISiteService : IScope
{
    Task CheckUserAndSite(string siteAudience, string userId);
    Task <long>CheckUserSiteWithGoogle(string userId, string frontUri);
}
