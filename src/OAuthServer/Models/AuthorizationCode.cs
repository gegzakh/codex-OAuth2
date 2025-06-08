namespace OAuthServer.Models;

public class AuthorizationCode
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string ClientId { get; set; }
    public required string RedirectUri { get; set; }
    public DateTime ExpiresAt { get; set; }
}
