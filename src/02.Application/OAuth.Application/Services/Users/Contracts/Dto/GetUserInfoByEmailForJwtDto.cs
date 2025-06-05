namespace OAuth.Application.Services.Users.Contracts.Dto;

public class GetUserInfoByEmailForJwtDto
{
    public string? Mobile { get; set; }
    public required string Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; init; }
    public long SietId { get; set; }
}
