using ApiRestProject.Model;

namespace ApiRestProject.Business;

public interface IPersonBusiness
{
  Person Create(Person person);
  Person FindById(long id);
  List<Person> findAll();
  Person Update(Person person);
  void Delete(long id);
}