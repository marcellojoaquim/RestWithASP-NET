using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestProject.Model;

[Table("person")]
public class Person
{

  [Column("id")]
  public long Id {get; set;}

  [Column("firstName")]
  public string FirstName {get; set;}

  [Column("lastName")]
  public string LastName {get; set;}

  [Column("adress")]
  public string Adress {get; set;}

  [Column("gender")]
  public string Gender {get; set;}

}