using ApiRestProject.Model;

namespace ApiRestProject.Repository;

public interface IBookRepository
{
  Book Create(Book book);
  Book FindById(long id);
  List<Book> findAll();
  Book Update(Book book);
  void Delete(long id);
  bool Exists(long id);
}