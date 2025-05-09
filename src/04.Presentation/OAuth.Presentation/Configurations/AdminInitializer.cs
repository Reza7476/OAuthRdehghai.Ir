using OAuth.Application.Handlers.Users.Contracts;

namespace OAuth.Presentation.Configurations;

public class AdminInitializer
{
    private readonly IServiceProvider _serviceProvider;

    public AdminInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Initialize()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var userHandler = scope.ServiceProvider.GetRequiredService<IUserHandler>();

            try
            {
                userHandler.EnsureAdministratorIsExist().Wait();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
