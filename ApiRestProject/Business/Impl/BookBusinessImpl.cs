using ApiRestProject.Data.Converter.Impl;
using ApiRestProject.Data.VO;
using ApiRestProject.Model;
using ApiRestProject.Repository.Generic;

namespace ApiRestProject.Business.Impl;

public class BookBusinessImpl : IBookBusiness
{
  private readonly IRepository<Book> _bookRepository;
  private readonly BookConverter _converter;

  public BookBusinessImpl(IRepository<Book> bookRepository)
  {
    _bookRepository = bookRepository;
    _converter = new BookConverter();
  }

  public BookVO Create(BookVO bookVO)
  {
    var bookEntity = _converter.Parse(bookVO);
    bookEntity = _bookRepository.Create(bookEntity);
    return _converter.Parse(bookEntity);
  }

  public void Delete(long id)
  {
    _bookRepository.Delete(id);
  }

  public List<BookVO> findAll()
  {

    return _converter.Parse(_bookRepository.findAll());
  }

  public BookVO FindById(long id)
  {
    return _converter.Parse(_bookRepository.FindById(id));
  }

  public BookVO Update(BookVO bookVO)
  {
    var bookEntity = _converter.Parse(bookVO);
    bookEntity = _bookRepository.Update(bookEntity);
    return _converter.Parse(bookEntity);
  }
}