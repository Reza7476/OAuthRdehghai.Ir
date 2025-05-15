namespace OAuth.Application.Services.Users.Contracts.Dto;

public class GetAllUsersDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string UserName { get; set; }
    public required string Mobile { get; set; }
}
