using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OAuth.Presentation.Configurations;

public static class JwtConfig
{
    public static IServiceCollection AddJwtAuthenticationConfig(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var issuer = configuration["jwt:issuer"];
        var audience = configuration["jwt:Audience"];
        var key = configuration["jwt:key"];
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = issuer,
                     ValidAudience = audience,
                 };
             });

        return services;
    }
}
