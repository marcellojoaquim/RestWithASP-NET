using ApiRestProject.Model;

namespace ApiRestProject.Business;

public interface IBookBusiness
{
  Book Create(Book boook);
  Book FindById(long id);
  List<Book> findAll();
  Book Update(Book book);
  void Delete(long id);
}