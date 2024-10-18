namespace Application.Responses;

public class AuthenticationResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresAt { get; set; }
}
