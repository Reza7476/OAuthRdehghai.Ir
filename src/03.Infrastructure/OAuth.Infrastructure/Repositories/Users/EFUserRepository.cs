using OAuth.Application.Services.Users.Contracts;
using Microsoft.EntityFrameworkCore;
using OAuth.Core.Entities.Users;
using OAuth.Application.Services.Users.Contracts.Dto;
using Microsoft.VisualBasic;

namespace OAuth.Infrastructure.Repositories.Users;

public class EFUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public EFUserRepository(EFDataContext context)
    {
        _users = context.Set<User>();
    }

    public async Task Add(User user)
    {
        await _users.AddAsync(user);
    }

    public async Task<List<GetAllUsersDto>> GetAll()
    {
        return await _users.Select(_ => new GetAllUsersDto
        {
            Id = _.Id,
            Name = _.Name,
            LastName = _.LastName,
            UserName = _.UserName,
            Mobile = _.Mobile

        }).ToListAsync();
    }

    public async Task<bool> IsExistByMobile(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }

    public async Task<bool> IsExistByUserName(string userName)
    {
        return await _users.AnyAsync(_ => _.UserName == userName);
    }

    public async Task<GetUserInfoForJwtDto?> IsExistByUserNameAndReturnUserInfoForJwt(string userName)
    {
        return await _users
            .Where(_ => _.UserName == userName)
            .Select(_ => new GetUserInfoForJwtDto()
            {
                Id= _.Id,
                Mobile=_.Mobile,
                HashPass=_.HashPassword,
                FirstName=_.Name,
                LastName=_.LastName,
            }).FirstOrDefaultAsync();
    }

    public async Task<bool> PasswordIsCorrect(string userId, string hashPass)
    {
        return await _users.AnyAsync(_ => _.Id == userId && _.HashPassword == hashPass);
    }
}
