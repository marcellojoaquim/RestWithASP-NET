using System;
using System.Security.Claims;

namespace ApiRestProject.Services;

public interface IToken
{

  string GenerateAccessToken(IEnumerable<Claim> claims);
  string GenerateRefreshToken();
  ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

}
