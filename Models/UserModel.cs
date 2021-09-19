using DuoEditor.Internal.Auth.Constants;

namespace DuoEditor.Internal.Auth.Models
{
  public class UserModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserStatus Status { get; set; }
    public string? ProfileImage { get; set; }

    public UserModel(int id, string name, string email, UserStatus status, string? profileImage)
    {
      Id = id;
      Name = name;
      Email = email;
      Status = status;
      ProfileImage = profileImage;
    }
  }
}