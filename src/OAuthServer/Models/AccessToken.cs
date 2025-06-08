namespace OAuthServer.Models;

public class AccessToken
{
    public int Id { get; set; }
    public required string Token { get; set; }
    public required string ClientId { get; set; }
    public DateTime ExpiresAt { get; set; }
}
