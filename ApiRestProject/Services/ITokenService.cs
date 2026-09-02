using System;
using System.Security.Claims;

namespace ApiRestProject.Services;

public interface ITokenService
{

  string GenerateAccessToken(IEnumerable<Claim> claims);
  string GenerateRefreshToken();
  ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

}
