using DuoEditor.Internal.Auth.Auth;
using DuoEditor.Internal.Auth.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace DuoEditor.Internal.Auth.Config
{
  public static class DIConfig
  {
    public static IServiceCollection AddAuth<T>(this IServiceCollection services)
      where T : class, ICacheRepository
    {
      services.AddScoped<ICacheRepository, T>();
      services.AddAuthentication("Jwt").AddScheme<AuthOptions, AuthHandler>("Jwt", opt => { });

      return services;
    }
  }
}