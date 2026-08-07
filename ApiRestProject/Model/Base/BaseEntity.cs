using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestProject.Model.Base;

public class BaseEntity
{
  [Column("id")]
  public long id {get; set;}
}