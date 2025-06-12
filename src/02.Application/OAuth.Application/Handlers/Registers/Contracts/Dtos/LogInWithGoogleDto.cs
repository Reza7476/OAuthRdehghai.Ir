namespace OAuth.Application.Handlers.Registers.Contracts.Dtos;

public class LogInWithGoogleDto
{
    public required string Email { get; set; }
    public  string? FullName { get; set; }
    public required string FrontUri { get; set; }
}
