using Microsoft.EntityFrameworkCore;
using OAuth.Application.Services.Roles.Contracts;
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

    public async Task<bool> IsExistByName(string roleName)
    {
        return await _roles.AnyAsync(_=>_.RoleName==roleName);
    }
}
