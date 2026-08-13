
using ApiRestProject.Data.Converter.Contract;
using ApiRestProject.Data.VO;
using ApiRestProject.Model;

namespace ApiRestProject.Data.Converter.Impl;

public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
{
  public Book Parse(BookVO origin)
  {
    if(origin == null) return null;
    return new Book
    {
      Id = origin.Id,
      Author = origin.Author,
      Title = origin.Title,
      Price = origin.Price,
      LaunchDate = origin.LaunchDate
    };
  }

  public List<Book> Parse(List<BookVO> origin)
  {
    if(origin == null) return null;
    return origin.Select(item => Parse(item)).ToList();
  }

  public BookVO Parse(Book origin)
  {
    if(origin == null) return null;
    return new BookVO
    {
      Id = origin.Id,
      Author = origin.Author,
      Title = origin.Title,
      Price = origin.Price,
      LaunchDate = origin.LaunchDate
    };
  }

  public List<BookVO> Parse(List<Book> origin)
  {
    if(origin == null) return null;
    return origin.Select(item => Parse(item)).ToList();
  }
}