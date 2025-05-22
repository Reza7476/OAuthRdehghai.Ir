using OAuth.Application.JwtTokenService.Dtos;
using OAuth.Common.Interfaces;

namespace OAuth.Application.JwtTokenService;

public interface IJwtTokenGenerator : IScope
{
    Task<string> GenerateToken(UserInfoForJwtDto dto);
}
