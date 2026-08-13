using ApiRestProject.Data.Converter.Impl;
using ApiRestProject.Data.VO;
using ApiRestProject.Model;
using ApiRestProject.Repository.Generic;

namespace ApiRestProject.Business.Impl;

public class PersonBusinessImpl : IPersonBusiness
{
  private readonly IRepository<Person> _personRepository;
  private readonly PersonConverter _converter;

  public PersonBusinessImpl(IRepository<Person> personRepository)
  {
    _personRepository = personRepository;
    _converter = new PersonConverter();
  }

  public PersonVO Create(PersonVO personVO)
  {
    var p = _converter.Parse(personVO);
    p =_personRepository.Create(p);
    return _converter.Parse(p);
  }

  public void Delete(long id)
  {
    _personRepository.Delete(id);
  }

  public List<PersonVO> findAll()
  {
    return _converter.Parse(_personRepository.findAll());
  }

  public PersonVO FindById(long id)
  {
    return _converter.Parse(_personRepository.FindById(id));
  }

  public PersonVO Update(PersonVO personVO)
  {
    var p = _converter.Parse(personVO);
    p =_personRepository.Update(p);
    return _converter.Parse(p);
  }
}