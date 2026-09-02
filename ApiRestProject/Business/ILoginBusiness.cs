using ApiRestProject.Data.VO;

namespace ApiRestProject.Business;
public interface ILoginBusiness
{
  TokenVO ValidateCredentials(UserVO userVO);
  TokenVO ValidateCredentials(TokenVO tokenVO);
}