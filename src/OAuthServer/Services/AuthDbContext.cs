using Microsoft.EntityFrameworkCore;
using OAuthServer.Models;

namespace OAuthServer.Services;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {}

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<AuthorizationCode> AuthorizationCodes => Set<AuthorizationCode>();
    public DbSet<AccessToken> AccessTokens => Set<AccessToken>();
}
