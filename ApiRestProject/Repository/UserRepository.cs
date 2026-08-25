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

  private string ComputeHash(string input, SHA256 algorithm)
  {
    Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
    Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
    return BitConverter.ToString(hashedBytes);
  }
}