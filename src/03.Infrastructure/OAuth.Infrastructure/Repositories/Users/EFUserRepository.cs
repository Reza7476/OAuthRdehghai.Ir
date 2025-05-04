using OAuth.Application.Services.Users.Contracts;
using Microsoft.EntityFrameworkCore;
using OAuth.Core.Entities.Users;

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

    public async Task<bool> IsExistByMobile(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }

    public async Task<bool> IsExistByUserName(string userName)
    {
        return await _users.AnyAsync(_ => _.UserName == userName);
    }
}
