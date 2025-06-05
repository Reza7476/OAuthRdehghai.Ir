using OAuth.Application.Handlers.Registers.Contracts.Dtos;
using OAuth.Common.Interfaces;

namespace OAuth.Application.Handlers.Google.Contracts;

public interface ILoginWithGoogleHandler : IService
{
    Task<string> LoginWithGoogle(LogInWithGoogleDto dto);
}
