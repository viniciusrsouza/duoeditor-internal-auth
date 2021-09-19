namespace DuoEditor.Internal.Auth.Models
{
  public class IntrospectionResponse
  {
    public UserModel User { get; set; } = null!;
    public long Expiration { get; set; }

    public IntrospectionResponse(UserModel user, long expiration)
    {
      User = user;
      Expiration = expiration;
    }
  }
}