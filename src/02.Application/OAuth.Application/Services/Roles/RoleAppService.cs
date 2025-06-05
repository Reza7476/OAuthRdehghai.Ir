using OAuth.Application.Services.Roles.Contracts;
using OAuth.Application.Services.Roles.Contracts.Dto;
using OAuth.Application.Services.Roles.Exceptions;
using OAuth.Common.Interfaces;
using OAuth.Core.Entities.Users;

namespace OAuth.Application.Services.Roles;

public class RoleAppService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleAppService(
        IRoleRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Add(AddRoleDto dto)
    {
        if (await _repository.IsExistByName(dto.RoleName))
            throw new RoleNameIsDuplicateException();

        var role = new Role()
        {
            RoleName = dto.RoleName,
        };

        await _repository.Add(role);
        await _unitOfWork.Complete();
        return role.Id;
    }

    public async Task AssignRoleToUser(string adminId, long roleId)
    {

        var newUserRole = new UserRole()
        {
            RoleId = roleId,
            UserId = adminId
        };
        await _repository.AssignRoleToUser(newUserRole);
        await _unitOfWork.Complete();

        await _repository.AssignRoleToUser(newUserRole);
    }

    public async Task CheckUserRoleWithGoogle(string userId, string roleName)
    {
        var roleId = await _repository.GetRoleIdByName(roleName);
        if (roleId > 0)
        {
            var userInRole = await _repository.IsUserInRole(userId, (long)roleId);
            if (userInRole)
            {

            }
            else
            {
                var newUserRole = new UserRole()
                {
                    RoleId = (long)roleId,
                    UserId = userId
                };
                
                await _repository.AssignRoleToUser(newUserRole);
                await _unitOfWork.Complete();
            }
        }
        else
        {
            var newRole = new Role()
            {
                RoleName = roleName,
                UserRoles = new List<UserRole>()
            };
            newRole.UserRoles.Add(new UserRole()
            {
                UserId = userId
            });

            await _repository.Add(newRole);
            await _unitOfWork.Complete();
        }
    }

    public async Task<List<string>> GetUserRolesByUserId(string userId)
    {
        return await _repository.GetUserRolesByUserId(userId);
    }
}
