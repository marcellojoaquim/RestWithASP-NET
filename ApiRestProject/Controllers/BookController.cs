using ApiRestProject.Business;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ApiRestProject.Data.VO;
using ApiRestProject.Hypermedia.Filters;

namespace ApiRestProject.Controllers;

[ApiVersion("1")]
[ApiController]
[Route("api/[controller]/v{version:apiVersion}")]
public class BookController : ControllerBase
{

    private readonly ILogger<BookController> _logger;
    private IBookBusiness _bookBusiness;

    public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
    {
        _logger = logger;
        _bookBusiness = bookBusiness;
    }

    [HttpGet]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        _logger.LogInformation("Chamando FindAll() books");
        
        return Ok(_bookBusiness.findAll());
    }

    [HttpGet("{id}")]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get(long id)
    {
        _logger.LogInformation("Chamando FindById() book");

        var person = _bookBusiness.FindById(id);
        if(person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post([FromBody] BookVO book)
    {
        _logger.LogInformation("Chamando Create() book");

        if(book == null)
        {
            return BadRequest();
        }
        return Ok(_bookBusiness.Create(book));
    }

    [HttpPut]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put([FromBody] BookVO book)
    {
        _logger.LogInformation("Chamando Update() book");

        if(book == null)
        {
            return BadRequest();
        }
        return Ok(_bookBusiness.Update(book));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _logger.LogInformation("Chamando Delete() book");

        _bookBusiness.Delete(id);
        
        return NoContent();
    }
}
