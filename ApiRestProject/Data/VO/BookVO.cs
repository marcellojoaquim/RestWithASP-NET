namespace ApiRestProject.Data.VO;

public class BookVO
{
  public long Id {get; set;}
  public string Author {get; set;}

  public DateOnly LaunchDate {get; set;}

  public double Price {get; set;}

  public string Title {get; set;}
}