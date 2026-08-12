using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiRestProject.Model.Base;

namespace ApiRestProject.Model;

[Table("books")]
public class Book: BaseEntity
{
  

  [Column("author")]
  [Required(ErrorMessage ="Author é obrigatório")]
  public string Author {get; set;}

  [Column("launch_date")]
  [Required(ErrorMessage ="LaunchDate é obrigatório")]
  public DateOnly LaunchDate {get; set;}

  [Column("price")]
  [Required(ErrorMessage ="Price é obrigatório")]
  public double Price {get; set;}

  [Column("title")]
  [Required(ErrorMessage ="Title é obrigatório")]
  public string Title {get; set;}
}