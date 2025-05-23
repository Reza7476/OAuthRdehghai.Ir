namespace OAuth.Application.Services.Users.Contracts.Dto;

public class GetUserInfoForJwtDto
{
    public required string  Mobile { get; set; }
    public required string Id { get; set; }
    public required string HashPass { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
