using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using DuoEditor.Internal.Auth.Cache;
using DuoEditor.Internal.Auth.Clients;
using DuoEditor.Internal.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DuoEditor.Internal.Auth.Auth
{
  public class AuthHandler : AuthenticationHandler<AuthOptions>
  {
    private readonly AuthClient _client;
    private readonly ICacheRepository _cache;

    public AuthHandler(AuthClient client, ICacheRepository cache, IOptionsMonitor<AuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
      _client = client;
      _cache = cache;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      var authorizationHeader = Context.Request.Headers["Authorization"];
      if (!authorizationHeader.Any())
      {
        return AuthenticateResult.Fail("No authentication token provided");
      }

      var token = authorizationHeader.ToString().Split(' ').LastOrDefault("");
      if (string.IsNullOrWhiteSpace(token))
      {
        return AuthenticateResult.Fail("No authentication token provided");
      }

      var cachedUser = await _cache.GetUser(token);

      if (cachedUser == null)
      {
        var response = await _client.Introspection(new IntrospectionRequest(token));
        if (response == null)
        {
          return AuthenticateResult.Fail("Unable to authenticate with provided credentials");
        }
        cachedUser = response.User;
        await _cache.SetUser(token, cachedUser, response.Expiration);
      }

      var claims = new[] {
          new Claim(ClaimTypes.Name, cachedUser.Id.ToString()),
          new Claim(ClaimTypes.Email, cachedUser.Email),
          new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(cachedUser))
      };

      var claimsIdentity = new ClaimsIdentity(claims, nameof(AuthHandler));
      var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);

      return AuthenticateResult.Success(ticket);
    }
  }
}