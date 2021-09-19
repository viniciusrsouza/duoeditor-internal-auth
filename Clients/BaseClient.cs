using DuoEditor.Internal.Auth.Config;
using Microsoft.Extensions.Options;

namespace DuoEditor.Internal.Auth.Clients
{
  public abstract class BaseClient<T> where T : BaseClientConfig
  {
    public readonly HttpClient _client;
    public readonly T _config;

    public BaseClient(HttpClient client, IOptions<T> options)
    {
      _config = options.Value;
      client.BaseAddress = new Uri(_config.ServiceUrl);
      _client = client;
    }

  }
}