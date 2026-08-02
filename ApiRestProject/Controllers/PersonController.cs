using ApiRestProject.Model;
using ApiRestProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{

    private readonly ILogger<PersonController> _logger;
    private IPersonService _personService;

    public PersonController(ILogger<PersonController> logger, IPersonService personService)
    {
        _logger = logger;
        _personService = personService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Chamando FindAll()");
        
        return Ok(_personService.findAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        _logger.LogInformation("Chamando FindById()");

        var person = _personService.FindById(id);
        if(person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Person person)
    {
        _logger.LogInformation("Chamando Create()");

        if(person == null)
        {
            return BadRequest();
        }
        return Ok(_personService.Create(person));
    }

    [HttpPut]
    public IActionResult Put([FromBody] Person person)
    {
        _logger.LogInformation("Chamando Update()");

        if(person == null)
        {
            return BadRequest();
        }
        return Ok(_personService.Update(person));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _logger.LogInformation("Chamando Delete()");

        _personService.Delete(id);
        
        return NoContent();
    }
}
