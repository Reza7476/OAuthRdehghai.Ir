using Microsoft.EntityFrameworkCore;
using OAuth.Application.Services.Roles.Contracts;
using OAuth.Application.Services.Roles.Contracts.Dto;
using OAuth.Core.Entities.Users;

namespace OAuth.Infrastructure.Repositories.Roles;

public class EFRoleRepository : IRoleRepository
{
    private readonly DbSet<Role> _roles;
    private readonly DbSet<UserRole> _userRoles;
    public EFRoleRepository(EFDataContext context)
    {
        _roles = context.Set<Role>();
        _userRoles = context.Set<UserRole>();
    }

    public async Task Add(Role role)
    {
        await _roles.AddAsync(role);
    }

    public async Task AssignRoleToUser(UserRole userRole)
    {
        await _userRoles.AddAsync(userRole);
    }

    public async Task<long?> GetRoleIdByName(string roleName)
    {
        return await _roles
            .Where(_ => _.RoleName == roleName)
            .Select(_ => _.Id)
            .FirstOrDefaultAsync(); ;
    }

    public async Task<GetRoleDto?> GetRoleInfoByName(string name)
    {
        return await _roles.Where(_ => _.RoleName == name).Select(_ => new GetRoleDto()
        {
            Id = _.Id,
            Name = _.RoleName
        }).FirstOrDefaultAsync();
    }

    public async Task<List<string>> GetUserRolesByUserId(string userId)
    {
        return await (
            from userRole in _userRoles
            join role in _roles
            on userRole.RoleId equals role.Id
            where userRole.UserId == userId
            select role.RoleName)
            .ToListAsync();
    }

    public async Task<bool> IsExistByName(string roleName)
    {
        return await _roles.AnyAsync(_ => _.RoleName == roleName);
    }

    public async Task<bool> IsUserInRole(string userId, long roleId)
    {
        return await _userRoles.AnyAsync(_=>_.UserId==userId && _.RoleId==roleId);
    }
}
