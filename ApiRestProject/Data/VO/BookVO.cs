using ApiRestProject.Hypermedia;
using ApiRestProject.Hypermedia.Abstract;

namespace ApiRestProject.Data.VO;

public class BookVO : ISupportsHypermedia
{
  public long Id {get; set;}
  public string Author {get; set;}

  public DateOnly LaunchDate {get; set;}

  public double Price {get; set;}

  public string Title {get; set;}
  public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}