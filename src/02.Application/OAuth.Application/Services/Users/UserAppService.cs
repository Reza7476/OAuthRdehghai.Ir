using OAuth.Application.Handlers.Registers.Exceptions;
using OAuth.Application.Services.Users.Contracts;
using OAuth.Application.Services.Users.Contracts.Dto;
using OAuth.Application.Services.Users.Exceptions;
using OAuth.Common.Interfaces;
using OAuth.Core.Entities.Users;

namespace OAuth.Application.Services.Users;

public class UserAppService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UserAppService(IUserRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Add(AddUserDto dto)
    {

        if (await _repository.IsExistByUserName(dto.UserName))
            throw new UserNameExistsException();

        if (await _repository.IsExistByMobile(dto.Mobile))
            throw new MobileIsExistException();


        var hasPass = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            LastName = dto.LastName,
            Mobile = dto.Mobile,
            Name = dto.Name,
            HashPassword = hasPass,
            UserName = dto.UserName,
            Email = dto.Email,
        };

        await _repository.Add(user);
        await _unitOfWork.Complete();

        return user.Id;
    }

    public async Task<string> AddUserByEmail(string email)
    {
        var userId = await _repository.GetUserIdByEmail(email);

        if (userId != null)
        {
            return userId;
        }
        else
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                CreationDate = DateTime.UtcNow,
                Email = email,
            };

            await _repository.Add(newUser);
            return newUser.Id;
        }

    }

    public async Task<GetUserInfoForJwtDto> CheckUserAndReturnUserInfoForJwt(
        string userName,
        string password)
    {
        var user = await _repository.GetByUserName(userName);
        if (user == null)
            throw new UserNotFoundException();

        if (!(BCrypt.Net.BCrypt.Verify(password, user.HashPassword)))
            throw new PasswordIsIncorrectException();

        return new GetUserInfoForJwtDto()
        {
            Id = user.Id,
            Mobile = user.Mobile,
            FirstName = user.Name,
            LastName = user.LastName,
        };
    }

    public async Task<List<GetAllUsersDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<GetUserInfoByEmailForJwtDto?> GetUserInfoByEmailAndSiteUrl(string email, string url)
    {
        return await _repository.GetUserInfoByEmailAndSiteUrl(email, url);
    }

    public async Task<List<string>> GetUserRoles(string id, long siteId)
    {
        return await _repository.GetUserRoles(id, siteId);
    }

    public async Task<bool> IsExistByUserName(string userName)
    {
        return await _repository.IsExistByUserName(userName);
    }
}
