using System.Text;
using ApiRestProject.Data.VO;
using ApiRestProject.Hypermedia.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestProject.Hypermedia.Enricher;

public class BookEnricher : ContentResponseEnricher<BookVO>
{
  protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper)
  {
    var path = "api/book";
    string link = GetLink(content.Id, urlHelper, path);

    content.Links.Add(new HyperMediaLink()
    {
      Action = HttpActionVerb.GET,
      Href = link,
      Rel = RelationType.self,
      Type = ResponseTypeFormat.DefaultGet
    });
    content.Links.Add(new HyperMediaLink()
    {
      Action = HttpActionVerb.POST,
      Href = link,
      Rel = RelationType.self,
      Type = ResponseTypeFormat.DefaultPost
    });
    content.Links.Add(new HyperMediaLink()
    {
      Action = HttpActionVerb.PUT,
      Href = link,
      Rel = RelationType.self,
      Type = ResponseTypeFormat.DefaultPut
    });
    content.Links.Add(new HyperMediaLink()
    {
      Action = HttpActionVerb.PATCH,
      Href = link,
      Rel = RelationType.self,
      Type = ResponseTypeFormat.DefaultPatch
    });
    content.Links.Add(new HyperMediaLink()
    {
      Action = HttpActionVerb.DELETE,
      Href = link,
      Rel = RelationType.self,
      Type = "int"
    });
    return Task.CompletedTask;
  }

  private string GetLink(long id, IUrlHelper urlHelper, string path)
  {
    lock (this)
    {
      var url = new {controller = path, id};
      return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
    }
  }
}