namespace ApiRestProject.Hypermedia.Abstract;

public interface ISupportsHypermedia
{
  List<HyperMediaLink> Links {get; set;} 
}