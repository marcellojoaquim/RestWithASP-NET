using ApiRestProject.Data.VO;
using ApiRestProject.Hypermedia.Utils;

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
  PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int currentPage);
}