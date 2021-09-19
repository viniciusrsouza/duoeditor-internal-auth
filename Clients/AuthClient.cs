using System.Net.Http.Json;
using DuoEditor.Internal.Auth.Config;
using DuoEditor.Internal.Auth.Models;
using Microsoft.Extensions.Options;

namespace DuoEditor.Internal.Auth.Clients
{
  public class AuthClient : BaseClient<AuthClientConfig>
  {
    public AuthClient(HttpClient client, IOptions<AuthClientConfig> options) : base(client, options)
    {
    }

    public async Task<IntrospectionResponse?> Introspection(IntrospectionRequest request)
    {
      try
      {
        var response = await _client.PostAsJsonAsync(_config.IntrospectionEndpoint, request);
        return await response.Content.ReadFromJsonAsync<IntrospectionResponse>();
      }
      catch
      {
        return null;
      }
    }
  }
}