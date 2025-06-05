namespace OAuth.Application.Services.Users.Contracts.Dto;

public class GetUserInfoDto
{
    public required string Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }
}
