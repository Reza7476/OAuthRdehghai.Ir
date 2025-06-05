using OAuth.Application.Services.Users.Contracts.Dto;
using OAuth.Common.Interfaces;
using OAuth.Core.Entities.Users;

namespace OAuth.Application.Services.Users.Contracts;

public interface IUserRepository : IRepository
{
    Task Add(User user);
    Task<List<GetAllUsersDto>> GetAll();
    Task<GetUserInfoDto?> GetUserInfoByEmail(string email);
    Task<bool> IsExistByMobile(string mobile);
    Task<bool> IsExistByUserName(string userName);
    Task<User?> GetByUserName(string userName);
    Task<bool> PasswordIsCorrect(string userId, string hashPass);
    Task<List<string>> GetUserRoles(string id, long siteId);
    
    Task <string?>GetUserIdByEmail(string email);
    Task<GetUserInfoByEmailForJwtDto?> GetUserInfoByEmailAndSiteUrl(string email, string url);
}
