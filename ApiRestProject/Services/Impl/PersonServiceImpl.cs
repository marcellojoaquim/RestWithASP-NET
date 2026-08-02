using ApiRestProject.Model;

namespace ApiRestProject.Services.Impl;

public class PersonServiceImpl : IPersonService
{
  private volatile int count;

  public Person Create(Person person)
  {
    return person;
  }

  public void Delete(long id)
  {
    
  }

  public List<Person> findAll()
  {
    List<Person> peaple = new List<Person>();
    for (int i = 0; i < 8; i++)
    {
      Person person = MockPerson(i);
      peaple.Add(person);
    }
    return peaple;
  }

  public Person FindById(long id)
  {
    return new Person
    {
      Id = IncrementAndGet(),
      FirstName = "Marcello",
      LastName = "Joaquim",
      Adress = "Rua capitão, Jaboatão, PE",
      Gender = "Male"
    };
  }

  public Person Update(Person person)
  {
    return person;
  }

  private Person MockPerson(int i)
  {
    return new Person
    {
      Id = IncrementAndGet(),
      FirstName = "Marcello "+i,
      LastName = "Joaquim "+i,
      Adress = $"Rua capitão {i}, Jaboatão, PE",
      Gender = "Male"
    };
  }

  private long IncrementAndGet()
  {
    return Interlocked.Increment(ref count);
  }
}