using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiRestProject.Configurations;
using ApiRestProject.Data.VO;
using ApiRestProject.Repository;
using ApiRestProject.Services;

namespace ApiRestProject.Business.Impl;

public class LoginBusinessImpl : ILoginBusiness
{

  private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ssss";
  private TokenConfiguration _configuration;
  private IUserRepository _repository;
  private readonly ITokenService _tokenService;

  public LoginBusinessImpl(TokenConfiguration tokenConfiguration, IUserRepository userRepository, ITokenService tokenService)
  {
    _configuration = tokenConfiguration;
    _repository = userRepository;
    _tokenService = tokenService;
  }

  public TokenVO ValidateCredentials(UserVO userVO)
  {
    var user = _repository.ValidateCredentials(userVO);
    if(user == null) return null;

    var claims = new List<Claim>
    {
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
      new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
    };

    var accessToken = _tokenService.GenerateAccessToken(claims);
    var refreshToken = _tokenService.GenerateRefreshToken();

    user.RefreshToken = refreshToken;
    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

    _repository.RefreshUserInfo(user);

    DateTime createDate = DateTime.Now;
    DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

    return new TokenVO(
      true,
      createDate.ToString(DATE_FORMAT),
      expirationDate.ToString(DATE_FORMAT),
      accessToken,
      refreshToken
    );
  }
}