using OAuth.Common.Interfaces;

namespace OAuth.Application.Handlers.Users.Contracts;

public interface IUserHandler : IScope
{
    Task EnsureAdministratorIsExist();
}
