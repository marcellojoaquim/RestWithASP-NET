using ApiRestProject.Model;
using ApiRestProject.Business;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ApiRestProject.Data.VO;

namespace ApiRestProject.Controllers;

[ApiVersion("1")]
[ApiController]
[Route("api/[controller]/v{version:apiVersion}")]
public class PersonController : ControllerBase
{

    private readonly ILogger<PersonController> _logger;
    private IPersonBusiness _personBusiness;

    public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
    {
        _logger = logger;
        _personBusiness = personBusiness;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Chamando FindAll()");
        
        return Ok(_personBusiness.findAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        _logger.LogInformation("Chamando FindById()");

        var person = _personBusiness.FindById(id);
        if(person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    public IActionResult Post([FromBody] PersonVO person)
    {
        _logger.LogInformation("Chamando Create()");

        if(person == null)
        {
            return BadRequest();
        }
        return Ok(_personBusiness.Create(person));
    }

    [HttpPut]
    public IActionResult Put([FromBody] PersonVO person)
    {
        _logger.LogInformation("Chamando Update()");

        if(person == null)
        {
            return BadRequest();
        }
        return Ok(_personBusiness.Update(person));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _logger.LogInformation("Chamando Delete()");

        _personBusiness.Delete(id);
        
        return NoContent();
    }
}
