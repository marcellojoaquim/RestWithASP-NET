using ApiRestProject.Model;
using ApiRestProject.Business;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ApiRestProject.Data.VO;
using ApiRestProject.Hypermedia.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ApiRestProject.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize("Bearer")]
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
    [ProducesResponseType(200, Type = typeof(List<PersonVO>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        _logger.LogInformation("Chamando FindAll()");
        
        return Ok(_personBusiness.findAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(PersonVO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [TypeFilter(typeof(HyperMediaFilter))]
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
    [ProducesResponseType(201, Type = typeof(PersonVO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [TypeFilter(typeof(HyperMediaFilter))]
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
    [ProducesResponseType(200, Type = typeof(PersonVO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [TypeFilter(typeof(HyperMediaFilter))]
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
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult Delete(long id)
    {
        _logger.LogInformation("Chamando Delete()");

        _personBusiness.Delete(id);
        
        return NoContent();
    }
}
