using OAuth.Application.Services.Users.Contracts.Dto;
using OAuth.Common.Interfaces;

namespace OAuth.Application.Services.Users.Contracts;

public interface IUserService : IService
{
    Task<string> Add(AddUserDto dto);
    Task<bool> IsExistByUserName(string v);
}
