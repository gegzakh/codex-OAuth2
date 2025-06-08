namespace OAuthServer.Models;

public class Client
{
    public int Id { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string RedirectUri { get; set; }
}
