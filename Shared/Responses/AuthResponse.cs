namespace Shared.Responses;

public class AuthResponse
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = default!;
    public string Token { get; set; } = default!;
}
