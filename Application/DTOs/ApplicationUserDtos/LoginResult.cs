namespace Application.DTOs.ApplicationUserDtos;

public class LoginResult
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpireAt { get; set; }
}