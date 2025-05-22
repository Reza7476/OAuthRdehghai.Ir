using OAuth.Application.Services.Sites.Contarcts;
using OAuth.Application.Services.Sites.Exceptions;

namespace OAuth.Application.Services.Sites;

public class SietAppService : ISiteService
{

    private readonly ISiteRepository _repository;

    public SietAppService(ISiteRepository siteRepository)
    {
        _repository = siteRepository;
    }

    public async Task CheckUserAndSite(string siteAudience, string userId)
    {
        var site = await _repository.GetSiteBySiteName(siteAudience);
        if (site == null)
            throw new SiteNotFoundException();
        if(!await _repository.checkUserSite(site.Id,userId))
            throw new SiteNotFoundException();
    }
}
