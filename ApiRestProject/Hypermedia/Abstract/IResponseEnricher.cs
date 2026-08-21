using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiRestProject.Hypermedia.Abstract;

public interface IResponseEnricher
{
  bool CanEnrich(ResultExecutingContext context);
  Task Enrich(ResultExecutingContext context);
} 