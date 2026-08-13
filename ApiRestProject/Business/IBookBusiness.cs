using ApiRestProject.Data.VO;

namespace ApiRestProject.Business;

public interface IBookBusiness
{
  BookVO Create(BookVO boook);
  BookVO FindById(long id);
  List<BookVO> findAll();
  BookVO Update(BookVO book);
  void Delete(long id);
}