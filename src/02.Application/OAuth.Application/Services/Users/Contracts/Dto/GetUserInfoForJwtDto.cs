namespace OAuth.Application.Services.Users.Contracts.Dto;

public class GetUserInfoForJwtDto
{
    public string  Mobile { get; set; }
    public string Id { get; set; }
    public string HashPass { get; set; }
}
