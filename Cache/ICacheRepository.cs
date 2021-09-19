using DuoEditor.Internal.Auth.Models;

namespace DuoEditor.Internal.Auth.Cache
{
  public interface ICacheRepository
  {
    Task<UserModel?> GetUser(string token);
    Task<UserModel?> SetUser(string token, UserModel user, long expiration);
  }
}