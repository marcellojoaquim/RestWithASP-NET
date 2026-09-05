using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiRestProject.Model.Base;

namespace ApiRestProject.Model;

[Table("person")]
public class Person : BaseEntity
{

  [Column("firstName")]
  [Required(ErrorMessage ="FirstName é obrigatório")]
  public string FirstName {get; set;}

  [Column("lastName")]
  [Required(ErrorMessage ="LastName é obrigatório")]
  public string LastName {get; set;}

  [Column("adress")]
  [Required(ErrorMessage ="Adress é obrigatório")]
  public string Adress {get; set;}

  [Column("gender")]
  [Required(ErrorMessage ="Gender é obrigatório")]
  public string Gender {get; set;}

  [Column("enabled")]
  public bool Enabled {get; set;}

}