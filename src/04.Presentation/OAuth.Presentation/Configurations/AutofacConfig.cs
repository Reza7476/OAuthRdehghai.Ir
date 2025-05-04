using Autofac;
using OAuth.Infrastructure;
using Autofac.Extensions.DependencyInjection;
using OAuth.Common.Interfaces;
using OAuth.Application.Services.Users;

namespace OAuth.Presentation.Configurations;

public static class AutofacConfig
{
    public static ConfigureHostBuilder AddAutofac(this ConfigureHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.ConfigureContainer<ContainerBuilder>(option =>
        {

            option.RegisterModule(new AutofacModule());
        });
        return builder;
    }
}


public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder container)
    {
        var applicationAssembly = typeof(UserAppService).Assembly;
        var presentationAssembly = typeof(AutofacConfig).Assembly;
        var infrastructureAssembly = typeof(EFDataContext).Assembly;

        container.RegisterAssemblyTypes(
            applicationAssembly,
            presentationAssembly,
            infrastructureAssembly)
            .Where(t => typeof(IScope).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();


        container.RegisterAssemblyTypes(infrastructureAssembly)
            .AssignableTo<IRepository>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        container.RegisterAssemblyTypes(presentationAssembly, applicationAssembly)
            .AssignableTo<IService>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        container.Register(ctx =>
        {
            var clientFactory = ctx.Resolve<IHttpClientFactory>();
            return clientFactory.CreateClient();
        }).As<HttpClient>().InstancePerLifetimeScope();

        base.Load(container);
    }
}
