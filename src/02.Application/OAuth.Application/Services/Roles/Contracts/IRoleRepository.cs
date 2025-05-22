using OAuth.Common.Interfaces;
using OAuth.Core.Entities.Users;

namespace OAuth.Application.Services.Roles.Contracts;

public interface IRoleRepository : IRepository
{
    Task Add(Role role);
    Task AssignRoleToUser(UserRole userRole);
    Task<List<string>> GetUserRolesByUserId(string userId);
    Task< bool> IsExistByName(string roleName);
}
