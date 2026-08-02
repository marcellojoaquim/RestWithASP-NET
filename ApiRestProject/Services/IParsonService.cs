using ApiRestProject.Model;

namespace ApiRestProject.Services;

public interface IPersonService
{
  Person Create(Person person);
  Person FindById(long id);
  List<Person> findAll();
  Person Update(Person person);
  void Delete(long id);
}