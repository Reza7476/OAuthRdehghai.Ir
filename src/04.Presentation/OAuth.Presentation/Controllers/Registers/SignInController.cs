using Microsoft.AspNetCore.Mvc;
using OAuth.Application.Handlers.Google.Contracts;
using OAuth.Application.Handlers.Registers.Contracts;
using OAuth.Application.Handlers.Registers.Contracts.Dtos;

namespace OAuth.Presentation.Controllers.Registers;

[ApiController]
[Route("api/SignIn")]
public class SignInController : Controller
{
    private readonly ISignInHandler _signHandler;
    private readonly ILoginWithGoogleHandler _loginWithGoogle;

    public SignInController(
        ISignInHandler signHandler,
        ILoginWithGoogleHandler loginWithGoogle)
    {
        _signHandler = signHandler;
        _loginWithGoogle = loginWithGoogle;
    }


    [HttpPost("login")]
    public async Task<string> SignIn(LogInDto dto)
    {
       
       return await _signHandler.LogIn(dto);
        
    }

    [HttpPost("logIn-with-google")]
    public async Task<string> LoginWithGoogle(LogInWithGoogleDto dto)
    {
        return await _loginWithGoogle.LoginWithGoogle(dto);
    }
}
