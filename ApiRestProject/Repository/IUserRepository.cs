using ApiRestProject.Data.VO;
using ApiRestProject.Model;

namespace ApiRestProject.Repository;

public interface IUserRepository
{
  User? ValidateCredentials(UserVO userVO);
  User? ValidateCredentials(string userName);
  User RefreshUserInfo(User user);

  bool RevokeToken(string userName);
}