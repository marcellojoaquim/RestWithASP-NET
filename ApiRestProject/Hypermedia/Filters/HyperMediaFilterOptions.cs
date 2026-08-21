using ApiRestProject.Hypermedia.Abstract;

namespace ApiRestProject.Hypermedia.Filters;

public class HyperMediaFilterOptions
{
  public List<IResponseEnricher> ContentResponseEnricherList {get; set;} = new List<IResponseEnricher>();
  
}