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
  public async Task<IActionResult> UploadOneFile(IFormFile file)
  {
    FileDetailVO detailVO = await _fileBusiness.SaveFileToDisk(file);
    return new OkObjectResult(detailVO);
  }

  [HttpPost("uploadMultipleFiles")]
  [Consumes("multipart/form-data")]
  [ProducesResponseType((200), Type = typeof(List<FileDetailVO>))]
  [ProducesResponseType(400)]
  [ProducesResponseType(401)]
  [Produces("application/json")]
  public async Task<IActionResult> UploadManyFiles(List<IFormFile> files)
  {
    List<FileDetailVO> detailVOList = await _fileBusiness.SaveFilesToDisk(files);
    return new OkObjectResult(detailVOList);
  }

  [HttpGet("downloadFile/{fileName}")]
  [Consumes("multipart/form-data")]
  [ProducesResponseType((200), Type = typeof(byte[]))]
  [ProducesResponseType(204)]
  [ProducesResponseType(400)]
  [ProducesResponseType(401)]
  [Produces("application/actet-stream")]
  public async Task<IActionResult> DownloadFileAsync(string fileName)
  {
    byte[] buffer = _fileBusiness.GetFile(fileName);

    if(buffer != null)
    {
      HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".", "")}";
      HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
      await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
    }
    return new ContentResult();
  }
}