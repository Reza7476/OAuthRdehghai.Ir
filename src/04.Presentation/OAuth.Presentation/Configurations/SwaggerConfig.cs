using Microsoft.OpenApi.Models;

namespace OAuth.Presentation.Configurations;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfigGen(this IServiceCollection service)
    {

        service.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            c.AddSecurityDefinition("Beare", new OpenApiSecurityScheme
            {
                Description= @"JWT را به این صورت وارد کنید: Bearer {token}",
                Name="Authorization",
                In=ParameterLocation.Header,
                Type=SecuritySchemeType.ApiKey,
                Scheme="Beare"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Beare"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return service;
    }
}
