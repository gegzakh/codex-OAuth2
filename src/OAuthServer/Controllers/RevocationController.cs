using Microsoft.AspNetCore.Mvc;
using OAuthServer.Services;

namespace OAuthServer.Controllers;

[ApiController]
[Route("oauth/revoke")]
public class RevocationController : ControllerBase
{
    private readonly AuthDbContext _db;

    public RevocationController(AuthDbContext db) => _db = db;

    [HttpPost]
    public IActionResult Revoke([FromForm] string token)
    {
        var accessToken = _db.AccessTokens.FirstOrDefault(t => t.Token == token);
        if (accessToken != null)
        {
            _db.AccessTokens.Remove(accessToken);
            _db.SaveChanges();
        }

        return Ok();
    }
}
