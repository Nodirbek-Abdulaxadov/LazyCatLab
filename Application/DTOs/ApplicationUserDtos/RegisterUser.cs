namespace Application.DTOs.ApplicationUserDtos;

public class RegisterUser : LoginUser
{
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;

    public List<string> Roles { get; set; } = new();
}