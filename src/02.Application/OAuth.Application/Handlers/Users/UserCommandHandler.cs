using BCrypt.Net;
using OAuth.Application.Handlers.Users.Contracts;
using OAuth.Application.Services.Roles.Contracts;
using OAuth.Application.Services.Roles.Contracts.Dto;
using OAuth.Application.Services.Users.Contracts;
using OAuth.Application.Services.Users.Contracts.Dto;
using OAuth.Core.Entities.Sites;
using OAuth.Core.Entities.Users;
using OAuth.Core.Entities.UserSites;

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
        var adminIsExist = await _userService.IsExistByUserName("Administrator");
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
        var hashPass = BCrypt.Net.BCrypt.HashPassword("#Reza1420");
        var administratorDto = new AddUserDto()
        {
            LastName = "Dehghani",
            Mobile = "+989174367476",
            Name = "Reza",
            UserName = "Administrator",
            Password= hashPass,
        };

        var userId = await _userService.Add(administratorDto);
        return userId;
    }


   

    //public bool VerifyLogin(string username, string password)
    //{
    //    var user = _context.Users.FirstOrDefault(u => u.Username == username);
    //    if (user == null || string.IsNullOrEmpty(user.PasswordHash))
    //    {
    //        return false;
    //    }

    //    return BCrypt.Verify(password, user.PasswordHash); // مقایسه رمز
    //}
}