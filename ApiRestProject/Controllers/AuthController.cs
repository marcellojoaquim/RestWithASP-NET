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
  private readonly ILogger<AuthController> _logger;
  private ILoginBusiness _loginBusiness;

  public AuthController(ILoginBusiness loginBusiness, ILogger<AuthController> logger)
  {
    _loginBusiness = loginBusiness;
    _logger = logger;
  }

  [HttpPost]
  [Route("signin")]
  public IActionResult Signin([FromBody] UserVO userVO)
  {
    _logger.LogInformation("Chamando Signin");
    if(userVO == null) return BadRequest("Invalid client request");

    var token = _loginBusiness.ValidateCredentials(userVO);
    if(token == null) return Unauthorized();

    return Ok(token);    
  }

  [HttpPost]
  [Route("refresh")]
  public IActionResult Refresh([FromBody] TokenVO tokenVO)
  {
    _logger.LogInformation("Chamando Refresh");
    if(tokenVO == null) return BadRequest("Invalid client request");

    var token = _loginBusiness.ValidateCredentials(tokenVO);
    if(token == null) return BadRequest("Invalid client request");

    return Ok(token); 
  }

}