using Microsoft.AspNetCore.Mvc;
using OAuthServer.Services;

namespace OAuthServer.Controllers;

[ApiController]
[Route("oauth/introspect")]
public class IntrospectionController : ControllerBase
{
    private readonly AuthDbContext _db;

    public IntrospectionController(AuthDbContext db) => _db = db;

    [HttpPost]
    public IActionResult Introspect([FromForm] string token)
    {
        var accessToken = _db.AccessTokens.FirstOrDefault(t => t.Token == token);
        if (accessToken == null)
            return Ok(new { active = false });

        var active = accessToken.ExpiresAt > DateTime.UtcNow;
        return Ok(new { active, exp = new DateTimeOffset(accessToken.ExpiresAt).ToUnixTimeSeconds() });
    }
}
