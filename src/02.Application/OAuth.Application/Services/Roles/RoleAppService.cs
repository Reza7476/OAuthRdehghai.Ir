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

    public async Task AssignRoleToUser(string userId, long roleId)
    {
        var userRole = new UserRole()
        {
            RoleId = roleId,
            UserId = userId,
        };
        await _repository.AssignRoleToUser(userRole);
        await _unitOfWork.Complete();
    }

    public async Task<List<string>> GetUserRolesByUserId(string userId)
    {
        return await _repository.GetUserRolesByUserId(userId);
    }
}
