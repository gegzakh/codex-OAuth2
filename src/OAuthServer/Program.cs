using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OAuthServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
// EF Core with configuration for SQL Server or PostgreSQL
builder.Services.AddDbContext<AuthDbContext>();
// Redis cache for tokens or sessions
builder.Services.AddSingleton<TokenCache>();

var app = builder.Build();

app.MapControllers();

app.Run();

