using OAuth.Application.Services.Users.Contracts.Dto;
using OAuth.Common.Interfaces;

namespace OAuth.Application.Services.Users.Contracts;

public interface IUserService : IService
{
    Task<string> Add(AddUserDto dto);
    Task<string> AddUserByEmail(string email);
    Task<GetUserInfoForJwtDto> CheckUserAndReturnUserInfoForJwt(string userName, string password);
    Task<List<GetAllUsersDto>> GetAll();
    Task<GetUserInfoByEmailForJwtDto?> GetUserInfoByEmailAndSiteUrl(string email, string frontUri);
    Task <List<string>> GetUserRoles(string id, long siteId);
    Task<bool> IsExistByUserName(string v);
}
