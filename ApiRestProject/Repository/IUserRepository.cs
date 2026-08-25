using ApiRestProject.Data.VO;
using ApiRestProject.Model;

namespace ApiRestProject.Repository;

public interface IUserRepository
{
  User ValidateCredentions(UserVO userVO);
}