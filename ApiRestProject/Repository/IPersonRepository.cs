using ApiRestProject.Data.VO;
using ApiRestProject.Model;
using ApiRestProject.Repository.Generic;

namespace ApiRestProject.Repository;

public interface IPersonRepository : IRepository<Person>
{
  Person Disable(long id);
  Person Enable(long id);
}