using Microsoft.AspNetCore.Mvc;
using OAuth.Application.Handlers.Registers.Contracts;
using OAuth.Application.Handlers.Registers.Contracts.Dtos;

namespace OAuth.Presentation.Controllers.Registers;

[ApiController]
[Route("api/SignIn")]
public class SignInController : Controller
{
    private readonly ISignInHandler _signHandler;

    public SignInController(ISignInHandler signHandler)
    {
        _signHandler = signHandler;
    }


    [HttpPost("login")]
    public async Task<string> SignIn(LogInDto dto)
    {
       
            return await _signHandler.LogIn(dto);
        
    }
}
