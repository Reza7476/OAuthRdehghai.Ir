using OAuth.Application.Services.Roles.Contracts.Dto;
using OAuth.Common.Interfaces;
using OAuth.Core.Entities.Users;

namespace OAuth.Application.Services.Roles.Contracts;

public interface IRoleRepository : IRepository
{
    Task Add(Role role);
    Task AssignRoleToUser(UserRole userRole);
    Task <long?>GetRoleIdByName(string roleName);
    Task<GetRoleDto?> GetRoleInfoByName(string v);
    Task<List<string>> GetUserRolesByUserId(string userId);
    Task< bool> IsExistByName(string roleName);
    Task<bool> IsUserInRole(string userId, long roleId);
}
