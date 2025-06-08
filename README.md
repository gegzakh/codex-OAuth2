# OAuth 2.0 Sample Server

This repository provides a minimal OAuth 2.0 authorization server written in ASP.NET Core 8 (compatible with upcoming .NET 9) using Entity Framework Core for persistence and Redis for caching.

## Endpoints

- `GET /oauth/authorize` — Authorization endpoint implementing the Authorization Code flow. Validates client information and issues an authorization code via redirect.
- `POST /oauth/token` — Token endpoint for exchanging an authorization code for an access token.
- `POST /oauth/introspect` — Token introspection endpoint returning token activity and expiration information.
- `POST /oauth/revoke` — Revocation endpoint to invalidate issued access tokens.

The server uses `AuthDbContext` (Entity Framework Core) to store clients, authorization codes, and access tokens. `TokenCache` demonstrates integration with Redis if token caching is desired.

The code is intentionally minimal and meant for educational purposes, showing how OAuth 2.0 APIs can be structured.
