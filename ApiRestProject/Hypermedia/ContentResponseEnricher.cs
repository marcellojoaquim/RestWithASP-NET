using System.Collections.Concurrent;
using System.Net.Mime;
using ApiRestProject.Hypermedia.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ApiRestProject.Hypermedia;

public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHypermedia
{

  public ContentResponseEnricher() { }
  public bool CanEnrich(Type contentType)
  {
    return contentType == typeof(T) || contentType == typeof(List<T>);
  }

  protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

  bool IResponseEnricher.CanEnrich(ResultExecutingContext context)
  {
    if (context.Result is OkObjectResult okObjectResult)
    {
      return CanEnrich(okObjectResult.Value.GetType());
    }
    return false;
  }

  public async Task Enrich(ResultExecutingContext context)
  {
    var urlHelper = new UrlHelperFactory().GetUrlHelper(context);
    if (context.Result is OkObjectResult okObjectResult)
    {
      if (okObjectResult.Value is T model)
      {
        await EnrichModel(model, urlHelper);
      }
      else if (okObjectResult.Value is List<T> collection)
      {
        ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
        Parallel.ForEach(bag, (element) =>
        {
          EnrichModel(element, urlHelper);
        });
      }
      await Task.FromResult<object>(null);
    }
  }
}