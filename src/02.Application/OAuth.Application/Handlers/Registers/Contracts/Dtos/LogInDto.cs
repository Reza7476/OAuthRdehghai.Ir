namespace OAuth.Application.Handlers.Registers.Contracts.Dtos;

public class LogInDto
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string SiteAudience { get; set; }
}
