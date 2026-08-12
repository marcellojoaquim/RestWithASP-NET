using ApiRestProject.Model;
using ApiRestProject.Repository;
using ApiRestProject.Repository.Generic;

namespace ApiRestProject.Business.Impl;

public class BookBusinessImpl : IBookBusiness
{
  private readonly IRepository<Book> _bookRepository;

  public BookBusinessImpl(IRepository<Book> bookRepository)
  {
    _bookRepository = bookRepository;
  }

  public Book Create(Book book)
  {
    return _bookRepository.Create(book);
  }

  public void Delete(long id)
  {
    _bookRepository.Delete(id);
  }

  public List<Book> findAll()
  {
    return _bookRepository.findAll();
  }

  public Book FindById(long id)
  {
    return _bookRepository.FindById(id);
  }

  public Book Update(Book book)
  {
    return _bookRepository.Update(book);
  }
}