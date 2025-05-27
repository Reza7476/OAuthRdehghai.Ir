using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using OAuth.Common.Exceptions;
using System.Net.Mime;
using System.Text.Json;

namespace OAuth.Presentation.Configurations;

public static class ExceptionHandlerStartUp
{
    public static IApplicationBuilder UseRezaExceptionHandler(this IApplicationBuilder app)
    {
        var environment = app.ApplicationServices
            .GetRequiredService<IWebHostEnvironment>();

        var jsonOptions = app.ApplicationServices
            .GetService<IOptions<JsonOptions>>()?.Value.SerializerOptions;

        app.UseExceptionHandler(_ => _.Run(async context =>
        {
            var exception = context.Features
                .Get<IExceptionHandlerPathFeature>()?.Error;

            var isAssignToCustomException = exception?.GetType()
                .IsAssignableTo(typeof(CustomException));

            const string errorProduction = "UnknownError";

            var result = new ExceptionErrorDto();

            if (!environment.IsDevelopment())
            {
                if (isAssignToCustomException == true)
                {
                    result.Error = exception?.GetType()
                         .Name.Replace("Exception", string.Empty);
                    result.Description = null;
                }
                else
                {
                    result.Error = errorProduction;
                    result.Description = null;
                }
            }
            else
            {
                result.Error = exception?.GetType().Name.Replace("Exception", string.Empty);
                result.Description = exception?.ToString();
            }
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(JsonSerializer.Serialize(result, jsonOptions));
        }));

        // if(environment.IsDevelopment()) app.UseHsts();


        return app;
    }
}
