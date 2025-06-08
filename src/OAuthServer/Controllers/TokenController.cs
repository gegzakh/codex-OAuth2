using Microsoft.AspNetCore.Mvc;
using OAuthServer.Models;
using OAuthServer.Services;

namespace OAuthServer.Controllers;

[ApiController]
[Route("oauth/token")]
public class TokenController : ControllerBase
{
    private readonly AuthDbContext _db;

    public TokenController(AuthDbContext db) => _db = db;

    [HttpPost]
    public IActionResult Token([FromForm] string grant_type, [FromForm] string code, [FromForm] string redirect_uri, [FromForm] string client_id, [FromForm] string client_secret)
    {
        var client = _db.Clients.FirstOrDefault(c => c.ClientId == client_id && c.ClientSecret == client_secret);
        if (client == null)
            return Unauthorized();

        if (grant_type == "authorization_code")
        {
            var authCode = _db.AuthorizationCodes.FirstOrDefault(a => a.Code == code && a.ClientId == client_id);
            if (authCode == null || authCode.RedirectUri != redirect_uri || authCode.ExpiresAt < DateTime.UtcNow)
                return BadRequest();

            _db.AuthorizationCodes.Remove(authCode);

            var token = new AccessToken
            {
                Token = Guid.NewGuid().ToString("N"),
                ClientId = client_id,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
            _db.AccessTokens.Add(token);
            _db.SaveChanges();

            return Ok(new { access_token = token.Token, token_type = "bearer", expires_in = 3600 });
        }

        return BadRequest();
    }
}
