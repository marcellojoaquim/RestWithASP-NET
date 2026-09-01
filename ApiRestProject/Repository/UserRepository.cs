using System.Security.Cryptography;
using System.Text;
using ApiRestProject.Data.VO;
using ApiRestProject.Model;
using ApiRestProject.Model.Context;

namespace ApiRestProject.Repository;

public class UserRepository : IUserRepository
{

  private readonly MySQLContext _context;

  public UserRepository(MySQLContext context)
  {
    _context = context;
  }

  public User ValidateCredentions(UserVO userVO)
  {
    var pass = ComputeHash(userVO.Password, SHA256.Create());
    return _context.Users.FirstOrDefault(u => (u.UserName == userVO.UserName) && (u.Password == pass));
  }

  public User RefreshUserInfo(User user)
  {
    if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

    var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
    if (result != null)
    {
      try
      {
        _context.Entry(result).CurrentValues.SetValues(user);
        _context.SaveChanges();
      }
      catch (Exception)
      {

        throw;
      }
    }

    return result;
  }

  private string ComputeHash(string input, SHA256 algorithm)
  {
    Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
    Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
    return BitConverter.ToString(hashedBytes);
  }
}