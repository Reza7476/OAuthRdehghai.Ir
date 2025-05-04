using OAuth.Common.Interfaces;
using OAuth.Core.Entities.Users;

namespace OAuth.Application.Services.Users.Contracts;

public interface IUserRepository : IRepository
{
    Task Add(User user);
    Task<bool> IsExistByMobile(string mobile);
    Task<bool> IsExistByUserName(string userName);
}
