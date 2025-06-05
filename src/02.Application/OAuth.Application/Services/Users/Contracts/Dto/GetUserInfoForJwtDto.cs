namespace OAuth.Application.Services.Users.Contracts.Dto;

public class GetUserInfoForJwtDto
{
    public  string?  Mobile { get; set; }
    public  required string Id { get; set; }
    public  string? FirstName { get; set; }
    public  string? LastName { get; set; }
}
