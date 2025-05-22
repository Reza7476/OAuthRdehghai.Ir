using OAuth.Application.Services.Roles.Contracts.Dto;
using OAuth.Common.Interfaces;

namespace OAuth.Application.Services.Roles.Contracts;

public interface IRoleService : IService
{
    Task<long> Add(AddRoleDto roleDto);
    Task AssignRoleToUser(string adminId, long roleId);
    Task<List<string>> GetUserRolesByUserId(string userId);
}
