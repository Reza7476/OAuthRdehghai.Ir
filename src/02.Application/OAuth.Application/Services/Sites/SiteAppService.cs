using OAuth.Application.Services.Sites.Contarcts;
using OAuth.Application.Services.Sites.Exceptions;
using OAuth.Common.Interfaces;
using OAuth.Core.Entities.Sites;
using OAuth.Core.Entities.UserSites;

namespace OAuth.Application.Services.Sites;

public class SiteAppService : ISiteService
{

    private readonly ISiteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;


    public SiteAppService(
        ISiteRepository siteRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = siteRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task AssignUserToSite(string userId, long siteId)
    {
        var userSite = new UserSite()
        {
            SiteId = siteId,
            UserId = userId
        };
        await _repository.AddUserSite(userSite);
        await _unitOfWork.Complete();
    }

    public async Task<bool> CheckSiteIsExist(string sietUrl)
    {
        return await _repository.IsExist(sietUrl);
    }

    public async Task CheckUserAndSite(string siteAudience, string userId)
    {
        var site = await _repository.GetSiteBySiteName(siteAudience);
        if (site == null)
            throw new SiteNotFoundException();
        if (!await _repository.checkUserSite(site.Id, userId))
            throw new SiteNotFoundException();
    }

    public async Task<long> CheckUserSiteWithGoogle(string userId, string url)
    {
        var siteId = await _repository.GetSiteIdBySiteUrl(url);
        if (siteId > 0)
        {
            var userIsInSite = await _repository.IsUserBelongToSite(userId, siteId);
            if (!userIsInSite)
            {
                var newUserSite = new UserSite()
                {
                    SiteId = (long)siteId,
                    UserId = userId,
                };
                await _repository.AddUserSite(newUserSite);
                await _unitOfWork.Complete();
            }
            return (long)siteId;
        }
        else
        {
            try
            {

                var newSite = new Site()
                {
                    SiteName = url,
                    SiteUrl = url
                };
                await _repository.Add(newSite);
                await _unitOfWork.CommitPartial();
                var newUserSite = new UserSite()
                {
                    SiteId = newSite.Id,
                    UserId = userId,
                };
                await _repository.AddUserSite(newUserSite);
                await _unitOfWork.Complete();
                return newSite.Id;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
