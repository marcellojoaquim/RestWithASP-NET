using ApiRestProject.Model.Base;

namespace ApiRestProject.Repository.Generic;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
  public T Create(T item)
  {
    throw new NotImplementedException();
  }

  public void Delete(long id)
  {
    throw new NotImplementedException();
  }

  public bool Exists(long id)
  {
    throw new NotImplementedException();
  }

  public List<T> findAll()
  {
    throw new NotImplementedException();
  }

  public T FindById(long id)
  {
    throw new NotImplementedException();
  }

  public T Update(T item)
  {
    throw new NotImplementedException();
  }
}