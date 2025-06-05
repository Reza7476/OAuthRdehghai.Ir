using Microsoft.EntityFrameworkCore;
using OAuth.Application.Services.Users.Contracts;
using OAuth.Application.Services.Users.Contracts.Dto;
using OAuth.Core.Entities.Sites;
using OAuth.Core.Entities.Users;
using OAuth.Core.Entities.UserSites;

namespace OAuth.Infrastructure.Repositories.Users;

public class EFUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly DbSet<UserRole> _userRoles;
    private readonly DbSet<Role> _roles;
    private readonly DbSet<Site> _sites;
    private readonly DbSet<UserSite> _userSites;

    public EFUserRepository(EFDataContext context)
    {
        _users = context.Set<User>();
        _userRoles = context.Set<UserRole>();
        _roles = context.Set<Role>();
        _userSites = context.Set<UserSite>();
        _sites = context.Set<Site>();
    }

    public async Task Add(User user)
    {
        await _users.AddAsync(user);
    }

    public async Task<List<GetAllUsersDto>> GetAll()
    {
        return await _users.Select(_ => new GetAllUsersDto
        {
            Id = _.Id,
            Name = _.Name,
            LastName = _.LastName,
            UserName = _.UserName,
            Mobile = _.Mobile

        }).ToListAsync();
    }

    public async Task<bool> IsExistByMobile(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }

    public async Task<bool> IsExistByUserName(string userName)
    {
        return await _users.AnyAsync(_ => _.UserName == userName);
    }

    public async Task<User?> GetByUserName(string userName)
    {
        return await _users
            .Where(_ => _.UserName == userName)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> PasswordIsCorrect(string userId, string hashPass)
    {
        return await _users.AnyAsync(_ => _.Id == userId && _.HashPassword == hashPass);
    }

    public Task<GetUserInfoDto?> GetUserInfoByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> GetUserRoles(string id, long siteId)
    {
        return await 
               (from userSite in _userSites
               join userRole in _userRoles
               on userSite.UserId equals userRole.UserId
               join role in _roles
               on userRole.RoleId equals role.Id
               where userSite.SiteId == siteId && userSite.UserId == id
               select role.RoleName).ToListAsync();
    }


    public async Task<string?> GetUserIdByEmail(string email)
    {
        var a = await _users
             .Where(_ => _.Email == email)
             .Select(_ => _.Id)
             .FirstOrDefaultAsync();
        return a;
    }

    public async Task<GetUserInfoByEmailForJwtDto?> GetUserInfoByEmailAndSiteUrl(string email, string url)
    {
        return await (from user in _users
                      join userSite in _userSites
                      on user.Id equals userSite.UserId
                      into userSites
                      from us in userSites.DefaultIfEmpty()
                      join site in _sites
                      on us.SiteId equals site.Id
                      where site.SiteUrl == url && user.Email == email
                      select new GetUserInfoByEmailForJwtDto()
                      {
                          Email = user.Email,
                          Id = user.Id,
                          FirstName = user.Name,
                          LastName = user.LastName,
                          Mobile = user.Mobile,
                          SietId = site.Id
                      }).FirstOrDefaultAsync();

    }
}
