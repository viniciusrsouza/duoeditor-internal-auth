namespace DuoEditor.Internal.Auth.Config
{
  public class AuthClientConfig : BaseClientConfig
  {
    public string IntrospectionEndpoint { get; set; } = null!;
  }
}