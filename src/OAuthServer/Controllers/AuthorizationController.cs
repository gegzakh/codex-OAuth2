using Microsoft.AspNetCore.Mvc;
using OAuthServer.Models;
using OAuthServer.Services;

namespace OAuthServer.Controllers;

[ApiController]
[Route("oauth/authorize")]
public class AuthorizationController : ControllerBase
{
    private readonly AuthDbContext _db;

    public AuthorizationController(AuthDbContext db) => _db = db;

    [HttpGet]
    public IActionResult Authorize([FromQuery] string response_type, [FromQuery] string client_id, [FromQuery] string redirect_uri)
    {
        var client = _db.Clients.FirstOrDefault(c => c.ClientId == client_id && c.RedirectUri == redirect_uri);
        if (client == null || response_type != "code")
            return BadRequest();

        var code = new AuthorizationCode
        {
            Code = Guid.NewGuid().ToString("N"),
            ClientId = client_id,
            RedirectUri = redirect_uri,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5)
        };
        _db.AuthorizationCodes.Add(code);
        _db.SaveChanges();

        return Redirect($"{redirect_uri}?code={code.Code}");
    }
}
