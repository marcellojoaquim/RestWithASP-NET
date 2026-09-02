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

  public User? ValidateCredentials(UserVO userVO)
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
  public User? ValidateCredentials(string userName)
  {
    return _context.Users.SingleOrDefault(u => u.UserName == userName);
  }

  private string ComputeHash(string input, HashAlgorithm algorithm)
  {
    Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
    Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
    
    var builder = new StringBuilder();

    foreach (var item in hashedBytes)
    {
      builder.Append(item.ToString("x2"));
    }

    return builder.ToString();
  }

}