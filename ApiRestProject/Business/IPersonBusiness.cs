using ApiRestProject.Data.VO;

namespace ApiRestProject.Business;

public interface IPersonBusiness
{
  PersonVO Create(PersonVO person);
  PersonVO FindById(long id);
  List<PersonVO> findAll();
  List<PersonVO> FindByName(string? firstName, string? secondName);
  PersonVO Update(PersonVO person);
  PersonVO Disable(long id);
  PersonVO Enable(long id);
  void Delete(long id);
}