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
        _logger.LogInformation("Chamando Person FindAll()");
        
        return Ok(_personBusiness.findAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(PersonVO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get(long id)
    {
        _logger.LogInformation("Chamando Person FindById()");

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
        _logger.LogInformation("Chamando Person Create()");

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
        _logger.LogInformation("Chamando Person Update()");

        if(person == null)
        {
            return BadRequest();
        }
        return Ok(_personBusiness.Update(person));
    }

    [HttpPatch("disable/{id}")]
    [ProducesResponseType(200, Type = typeof(PersonVO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Disable(long id)
    {
        _logger.LogInformation("Chamando Person Disabled()");

        var person = _personBusiness.Disable(id);
        return Ok(person);
    }

    [HttpPatch("enable/{id}")]
    [ProducesResponseType(200, Type = typeof(PersonVO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Enable(long id)
    {
        _logger.LogInformation("Chamando Person Enable()");

        var person = _personBusiness.Enable(id);
        return Ok(person);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult Delete(long id)
    {
        _logger.LogInformation("Chamando Person Delete()");

        _personBusiness.Delete(id);
        
        return NoContent();
    }
}
