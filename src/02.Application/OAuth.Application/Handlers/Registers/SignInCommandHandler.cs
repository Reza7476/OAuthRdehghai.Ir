using OAuth.Application.Handlers.Registers.Contracts;
using OAuth.Application.Handlers.Registers.Contracts.Dtos;
using OAuth.Application.JwtTokenService;
using OAuth.Application.JwtTokenService.Dtos;
using OAuth.Application.Services.Roles.Contracts;
using OAuth.Application.Services.Sites.Contarcts;
using OAuth.Application.Services.Users.Contracts;

namespace OAuth.Application.Handlers.Registers;

public class SignInCommandHandler : ISignInHandler
{

    private readonly IUserService _userService;
    private readonly ISiteService _siteService;
    private readonly IRoleService _roleService;
    private readonly IJwtTokenGenerator _jwtTokenService;

    public SignInCommandHandler(
        IUserService userService,
        ISiteService siteService,
        IRoleService roleService,
        IJwtTokenGenerator jwtTokenService)
    {
        _userService = userService;
        _siteService = siteService;
        _roleService = roleService;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<string> LogIn(LogInDto dto)
    {
       var userInfo = await _userService.CheckUserAndReturnUserInfoForJwt(dto.UserName, dto.Password);
       await _siteService.CheckUserAndSite(dto.SiteAudience, userInfo.Id);
       var getUserRole = await _roleService.GetUserRolesByUserId(userInfo.Id);
        var jwtToken = await _jwtTokenService.GenerateToken(new UserInfoForJwtDto
        {
            Mobile=userInfo.Mobile,
            UserId=userInfo.Id,
            UserRole=getUserRole,
            SiteAudience=dto.SiteAudience,
            FirstName=userInfo.FirstName,
            LastName=userInfo.LastName,
        });
        return jwtToken;


    }
}
