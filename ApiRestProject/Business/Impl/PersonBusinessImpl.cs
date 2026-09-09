using ApiRestProject.Data.Converter.Impl;
using ApiRestProject.Data.VO;
using ApiRestProject.Hypermedia.Utils;
using ApiRestProject.Model;
using ApiRestProject.Repository;
using ApiRestProject.Repository.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApiRestProject.Business.Impl;

public class PersonBusinessImpl : IPersonBusiness
{
  private readonly IPersonRepository _personRepository;
  private readonly PersonConverter _converter;

  public PersonBusinessImpl(IPersonRepository personRepository)
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

  public PersonVO Disable(long id)
  {
    var person = _personRepository.Disable(id);
    return _converter.Parse(person);
  }

  public PersonVO Enable(long id)
  {
    var person = _personRepository.Enable(id);
    return _converter.Parse(person);
  }

  public List<PersonVO> findAll()
  {
    return _converter.Parse(_personRepository.findAll());
  }

  public PersonVO FindById(long id)
  {
    return _converter.Parse(_personRepository.FindById(id));
  }

  public List<PersonVO> FindByName(string? firstName, string? secondName)
  {
    return _converter.Parse(_personRepository.FindByName(firstName, secondName));
  }

  public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int currentPage)
  {
    var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
    var size = (pageSize < 1) ? 10 : pageSize;
    var offset = currentPage > 0 ? (currentPage - 1) * size : 0;

    string query = @"SELECT * FROM person p WHERE 1 = 1";
    
    if(!string.IsNullOrWhiteSpace(name)) query = query + $" AND p.firstName like '%{name}%'";
    
    query += $"ORDER BY p.firstName {sort} LIMIT {size} OFFSET {offset}";

    string countQuery = @"SELECT COUNT(*) FROM person p WHERE 1 = 1";
    if(!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" AND p.firstName like '%{name}%'";

    var peaple = _personRepository.FindWithPagedSearch(query);
    int totalResult = _personRepository.GetCount(countQuery);

    return new PagedSearchVO<PersonVO>
    {
      CurrentPage = currentPage,
      List = _converter.Parse(peaple),
      PageSize = size,
      SortDirections = sort,
      TotalResults = totalResult
    };
  }

  public PersonVO Update(PersonVO personVO)
  {
    var p = _converter.Parse(personVO);
    p =_personRepository.Update(p);
    return _converter.Parse(p);
  }
}