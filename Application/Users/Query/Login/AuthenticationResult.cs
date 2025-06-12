
namespace Application.Users.Query.Login;

public class AuthenticationResult
{
    public string Token { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public Guid UserId { get; set; }
}