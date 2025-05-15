using OAuth.Application.Services.Users.Contracts.Dto;
using OAuth.Common.Interfaces;
using OAuth.Core.Entities.Users;

namespace OAuth.Application.Services.Users.Contracts;

public interface IUserRepository : IRepository
{
    Task Add(User user);
    Task<List<GetAllUsersDto>> GetAll();
    Task<bool> IsExistByMobile(string mobile);
    Task<bool> IsExistByUserName(string userName);
}
