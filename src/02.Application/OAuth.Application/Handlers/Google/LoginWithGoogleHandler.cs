using OAuth.Application.Handlers.Google.Contracts;
using OAuth.Application.Handlers.Registers.Contracts.Dtos;
using OAuth.Application.JwtTokenService;
using OAuth.Application.JwtTokenService.Dtos;
using OAuth.Application.Services.Roles.Contracts;
using OAuth.Application.Services.Sites.Contarcts;
using OAuth.Application.Services.Users.Contracts;

namespace OAuth.Application.Handlers.Google;

public class LoginWithGoogleHandler : ILoginWithGoogleHandler
{
    private readonly IUserService _userService;
    private readonly IJwtTokenGenerator _jwtService;
    private readonly ISiteService _siteService;
    private readonly IRoleService _roleService;
    public LoginWithGoogleHandler(
        IUserService userService,
        IJwtTokenGenerator jwtService,
        ISiteService siteService,
        IRoleService roleService)
    {
        _userService = userService;
        _jwtService = jwtService;
        _siteService = siteService;
        _roleService = roleService;
    }



    public async Task<string> LoginWithGoogle(LogInWithGoogleDto dto)
    {
        var userInfo = await _userService.GetUserInfoByEmailAndSiteUrl(dto.Email, dto.FrontUri);
        if (userInfo != null)
        {
            var userRoles = await _userService.GetUserRoles(userInfo.Id,userInfo.SietId);
            var jwtToken = await _jwtService.GenerateToken(new UserInfoForJwtDto()
            {
                
                FirstName = userInfo.FirstName?? dto.FullName,
                LastName = userInfo.LastName?? " ",
                Mobile = userInfo.Mobile?? "null",
                SiteAudience = dto.FrontUri,
                UserId = userInfo.Id,
                UserRole = userRoles,
                Email=dto.Email?? " null",
            });
            return jwtToken;
        }
        else
        {
            var userId = await _userService.AddUserByEmail(dto.Email);

          var siteId=  await _siteService.CheckUserSiteWithGoogle(userId, dto.FrontUri);
            await _roleService.CheckUserRoleWithGoogle(userId, SystemRole.OAuthGuest);

            var getNewUserInfo = await _userService.GetUserInfoByEmailAndSiteUrl(dto.Email, dto.FrontUri);
            var userRoles = await _userService.GetUserRoles(getNewUserInfo.Id, siteId);
            var jwtToken = await _jwtService.GenerateToken(new UserInfoForJwtDto()
            {
                FirstName = getNewUserInfo.FirstName ?? dto.FullName,
                LastName = getNewUserInfo.LastName ?? " ",
                Mobile = getNewUserInfo.Mobile ?? "null",
                SiteAudience = dto.FrontUri,
                UserId = getNewUserInfo.Id,
                UserRole = userRoles,
                Email=dto.Email?? "null"
            });
            return jwtToken;
        }
    }
}
