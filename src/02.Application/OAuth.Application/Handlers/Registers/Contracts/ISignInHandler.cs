using OAuth.Application.Handlers.Registers.Contracts.Dtos;
using OAuth.Common.Interfaces;

namespace OAuth.Application.Handlers.Registers.Contracts;

public interface ISignInHandler : IScope
{
    Task<string> LogIn(LogInDto dto);
}
