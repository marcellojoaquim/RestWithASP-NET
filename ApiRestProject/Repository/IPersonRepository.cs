using ApiRestProject.Model;

namespace ApiRestProject.Repository;

public interface IPersonRepository
{
  Person Create(Person person);
  Person FindById(long id);
  List<Person> findAll();
  Person Update(Person person);
  void Delete(long id);
  bool Exists(long id);
}