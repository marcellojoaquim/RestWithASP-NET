using ApiRestProject.Data.Converter.Contract;
using ApiRestProject.Data.VO;
using ApiRestProject.Model;

namespace ApiRestProject.Data.Converter.Impl;

public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
{
  public Person Parse(PersonVO origin)
  {
    if(origin == null) return null;
    return new Person
    {
      Id = origin.Id,
      FirstName = origin.FirstName,
      LastName = origin.LastName,
      Adress = origin.Adress,
      Gender = origin.Gender,
      Enabled = origin.Enabled
    };
    
  }

  public List<Person> Parse(List<PersonVO> origin)
  {
    if(origin == null) return null;
    return origin.Select(item => Parse(item)).ToList();
  }

  public PersonVO Parse(Person origin)
  {
    if(origin == null) return null;
    return new PersonVO
    {
      Id = origin.Id,
      FirstName = origin.FirstName,
      LastName = origin.LastName,
      Adress = origin.Adress,
      Gender = origin.Gender,
      Enabled = origin.Enabled
    };
  }

  public List<PersonVO> Parse(List<Person> origin)
  {
    if(origin == null) return null;
    return origin.Select(item => Parse(item)).ToList();
  }
}