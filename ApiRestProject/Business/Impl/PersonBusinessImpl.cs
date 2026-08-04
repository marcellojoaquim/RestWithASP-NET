using ApiRestProject.Model;
using ApiRestProject.Model.Context;
using ApiRestProject.Repository;

namespace ApiRestProject.Business.Impl;

public class PersonBusinessImpl : IPersonBusiness
{
  private readonly IPersonRepository _personRepository;

  public PersonBusinessImpl(IPersonRepository personRepository)
  {
    _personRepository = personRepository;
  }

  public Person Create(Person person)
  {
    return _personRepository.Create(person);
  }

  public void Delete(long id)
  {
    _personRepository.Delete(id);
  }

  public List<Person> findAll()
  {
    return _personRepository.findAll();
  }

  public Person FindById(long id)
  {
    return _personRepository.FindById(id);
  }

  public Person Update(Person person)
  {
    return _personRepository.Update(person);
  }
}