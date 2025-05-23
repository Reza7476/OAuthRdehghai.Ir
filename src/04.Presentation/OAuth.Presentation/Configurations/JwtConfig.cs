namespace OAuth.Presentation.Configurations;

public static class JwtConfig
{
    public static IServiceCollection AddJwtAuthontecation(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtKey = configuration["Jwt:Key"];
        //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer(options =>
        //{
        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidIssuer = "OAuth.rdehghai.ir",
        //        ValidateAudience = false,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = key
        //    };
        //});

        return services;
    }
}
