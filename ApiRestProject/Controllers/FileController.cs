using ApiRestProject.Business;
using ApiRestProject.Data.VO;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestProject.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize("Bearer")]
[Route("api/[controller]/v{version:apiVersion}")]
public class FileController : ControllerBase
{
  private readonly IFileBusiness _fileBusiness;

  public FileController(IFileBusiness fileBusiness)
  {
    _fileBusiness = fileBusiness;
  }

  [HttpPost("uploadFile")]
  [Consumes("multipart/form-data")]
  [ProducesResponseType((200), Type = typeof(FileDetailVO))]
  [ProducesResponseType(400)]
  [ProducesResponseType(401)]
  [Produces("application/json")]
  public async Task<IActionResult> UploadOndeFile(IFormFile file)
  {
    FileDetailVO detailVO = await _fileBusiness.SaveFileToDisk(file);
    return new OkObjectResult(detailVO);
  }
}