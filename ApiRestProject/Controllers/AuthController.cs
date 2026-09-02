using ApiRestProject.Business;
using ApiRestProject.Business.Impl;
using ApiRestProject.Data.VO;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ApiRestProject.Controllers;

[ApiVersion("1")]
[Route("api/[controller]/v{version:apiVersion}")]
[ApiController]
public class AuthController : ControllerBase
{
  private ILoginBusiness _loginBusiness;

  public AuthController(ILoginBusiness loginBusiness)
  {
    _loginBusiness = loginBusiness;
  }

  [HttpPost]
  [Route("signin")]
  public IActionResult Signin([FromBody] UserVO userVO)
  {

    if(userVO == null) return BadRequest("Invalid client request");

    var token = _loginBusiness.ValidateCredentials(userVO);
    if(token == null) return Unauthorized();

    return Ok(token);    
  }

}