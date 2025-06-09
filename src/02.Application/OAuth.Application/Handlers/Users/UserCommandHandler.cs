using OAuth.Application.Handlers.Users.Contracts;
using OAuth.Application.Services.Roles.Contracts;
using OAuth.Application.Services.Roles.Contracts.Dto;
using OAuth.Application.Services.Users.Contracts;
using OAuth.Application.Services.Users.Contracts.Dto;

namespace OAuth.Application.Handlers.Users;

public class UserCommandHandler : IUserHandler
{

    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public UserCommandHandler(
        IUserService userService,
        IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    public async Task EnsureAdministratorIsExist()
    {
        if (!await _userService.IsExistByUserName("Administrator"))
        {
            var adminId = await CreateUserAsAdministrator();
            var roleId = await CreateAdministratorAsRole();
            await AssignRoleToUser(adminId, roleId);
        }
    }

    private async Task AssignRoleToUser(
        string adminId,
        long roleId)
    {
        await _roleService.AssignRoleToUser(adminId, roleId);
    }

    private async Task<long> CreateAdministratorAsRole()
    {
        var roleDto = new AddRoleDto()
        {
            RoleName = "Administrator",
        };

        var roleId = await _roleService.Add(roleDto);
        return roleId;
    }

    private async Task<string> CreateUserAsAdministrator()
    {
        var administratorDto = new AddUserDto()
        {
            LastName = "Dehghani",
            Mobile = "+989174367476",
            Name = "Reza",
            Password = "123",
            UserName = "Administrator",
            Email = "rdehghani.akorn@gmail.com"
        };

        var userId = await _userService.Add(administratorDto);

        return userId;
    }
}
