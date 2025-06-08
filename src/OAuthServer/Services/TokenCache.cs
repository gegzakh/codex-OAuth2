using StackExchange.Redis;

namespace OAuthServer.Services;

public class TokenCache
{
    private readonly IDatabase _db;

    public TokenCache(IConnectionMultiplexer multiplexer)
    {
        _db = multiplexer.GetDatabase();
    }

    public async Task SetTokenAsync(string key, string token, TimeSpan lifetime)
        => await _db.StringSetAsync(key, token, lifetime);

    public async Task<string?> GetTokenAsync(string key)
        => await _db.StringGetAsync(key);
}
