using ApiRestProject.Model;
using ApiRestProject.Repository;
using ApiRestProject.Repository.Generic;

namespace ApiRestProject.Business.Impl;

public class PersonBusinessImpl : IPersonBusiness
{
  private readonly IRepository<Person> _personRepository;

  public PersonBusinessImpl(IRepository<Person> personRepository)
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