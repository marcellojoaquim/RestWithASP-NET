using ApiRestProject.Model.Base;
using ApiRestProject.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiRestProject.Repository.Generic;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{

  protected MySQLContext _context;

  private DbSet<T> dataSet;

  public GenericRepository(MySQLContext context)
  {
    _context = context;
    dataSet= context.Set<T>();
  }
  public T Create(T item)
  {
    try
    {
      dataSet.Add(item);
      _context.SaveChanges();
    }
    catch (Exception)
    {

      throw;
    }
    return item;
  }

  public void Delete(long id)
  {
    var result = dataSet.SingleOrDefault(p => p.Id.Equals(id));
    if (result != null)
    {
      try
      {
        dataSet.Remove(result);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        throw;
      }
    }
  }

  public bool Exists(long id)
  {
    return dataSet.Any(p => p.Id.Equals(id));
  }

  public List<T> findAll()
  {
    return dataSet.ToList();
  }

  public T FindById(long id)
  {
    return dataSet.SingleOrDefault(p => p.Id.Equals(id));
  }

  public List<T> FindWithPagedSearch(string query)
  {
    return dataSet.FromSqlRaw<T>(query).ToList();
  }

  public int GetCount(string query)
  {
    var result = "";
    using (var connection = _context.Database.GetDbConnection())
    {
      connection.Open();
      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;
        result = command.ExecuteScalar().ToString();
      }
    }
    return int.Parse(result);
  }

  public T Update(T item)
  {
    if (!Exists(item.Id)) return null;

    var result = dataSet.SingleOrDefault(p => p.Id.Equals(item.Id));
    if (result != null)
    {
      try
      {
        _context.Entry(result).CurrentValues.SetValues(item);
        _context.SaveChanges();
      }
      catch (Exception)
      {

        throw;
      }
    }

    return item;
  }
}