using ApiRestProject.Data.VO;

namespace ApiRestProject.Business;

public interface IPersonBusiness
{
  PersonVO Create(PersonVO person);
  PersonVO FindById(long id);
  List<PersonVO> findAll();
  PersonVO Update(PersonVO person);

  PersonVO Disable(long id);
  PersonVO Enable(long id);
  void Delete(long id);
}