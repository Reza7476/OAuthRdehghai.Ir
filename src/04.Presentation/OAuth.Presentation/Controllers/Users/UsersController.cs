using Microsoft.AspNetCore.Mvc;
using OAuth.Application.Services.Users.Contracts;
using OAuth.Application.Services.Users.Contracts.Dto;

namespace OAuth.Presentation.Controllers.Users;


[ApiController]
[Route("api/users")]
public class UsersController : Controller
{

    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<string> Add(AddUserDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpGet("all")]
    public async Task<List<GetAllUsersDto>> GetAll()
    {
        return await _service.GetAll();
    }

}
