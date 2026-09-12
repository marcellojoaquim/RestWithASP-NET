using System.Security.AccessControl;
using ApiRestProject.Data.VO;

namespace ApiRestProject.Business.Impl;

public class FileBusinessImpl : IFileBusiness
{
  private readonly string _basePath;
  private readonly IHttpContextAccessor _context;

  public FileBusinessImpl(IHttpContextAccessor accessor)
  {
    _context = accessor;
    _basePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadDir");
  }
  
  public byte[] GetFile(string fileName)
  {
    throw new NotImplementedException();
  }

  public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
  {
    FileDetailVO fileDetail = new FileDetailVO();
    var fileType = Path.GetExtension(file.FileName);
    var baseUrl = _context.HttpContext.Request.Host;
    var allowedExtentions = new[] {".pdf", ".jpg", ".png", ".jpeg"};
    
    if(allowedExtentions.Contains(fileType.ToLower()))
    {
      var docName = Path.GetFileName(file.FileName);
      if(file != null && file.Length > 0)
      {
        var destination = Path.Combine(_basePath, "", docName);
        fileDetail.DocumentName = docName;
        fileDetail.DocType = fileType;
        fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocumentName);

        using var stream = new FileStream(destination, FileMode.Create);
        await file.CopyToAsync(stream);
      }
    }
  
    return fileDetail;
  }

  public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
  {
    throw new NotImplementedException();
  }

}