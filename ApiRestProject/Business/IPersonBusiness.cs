using ApiRestProject.Data.VO;
using ApiRestProject.Model;

namespace ApiRestProject.Business;

public interface IPersonBusiness
{
  PersonVO Create(PersonVO person);
  PersonVO FindById(long id);
  List<PersonVO> findAll();
  PersonVO Update(PersonVO person);
  void Delete(long id);
}