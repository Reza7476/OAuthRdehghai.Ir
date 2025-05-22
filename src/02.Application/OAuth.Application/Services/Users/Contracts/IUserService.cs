using OAuth.Application.Services.Users.Contracts.Dto;
using OAuth.Common.Interfaces;

namespace OAuth.Application.Services.Users.Contracts;

public interface IUserService : IService
{
    Task<string> Add(AddUserDto dto);
    Task<GetUserInfoForJwtDto> CheckUserAndReturnUserInfoForJwt(string userName, string password);
    Task<List<GetAllUsersDto>> GetAll();
    Task<bool> IsExistByUserName(string v);
   
}
