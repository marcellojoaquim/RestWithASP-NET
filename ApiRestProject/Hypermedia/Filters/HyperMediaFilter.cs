using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiRestProject.Hypermedia.Filters;

public class HyperMediaFilter : ResultFilterAttribute
{
  private readonly HyperMediaFilterOptions _hyperMediaFilterOptions;

  public HyperMediaFilter(HyperMediaFilterOptions hyperMediaFilterOptions)
  {
    _hyperMediaFilterOptions = hyperMediaFilterOptions;
  }

  public override void OnResultExecuting(ResultExecutingContext resultExecutingContext)
  {
    TryEnrichResult(resultExecutingContext);
    base.OnResultExecuting(resultExecutingContext);
  }

  private void TryEnrichResult(ResultExecutingContext resultExecutingContext)
  {
    if(resultExecutingContext.Result is OkObjectResult objectResult)
    {
      var enricher = _hyperMediaFilterOptions.ContentResponseEnricherList
                                  .FirstOrDefault(x => x.CanEnrich(resultExecutingContext));
      if(enricher != null)
      {
        Task.FromResult(enricher.Enrich(resultExecutingContext));
      }

    }
  }
}