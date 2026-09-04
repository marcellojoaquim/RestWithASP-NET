using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiRestProject.Model.Base;

namespace ApiRestProject.Model;

[Table("users")]
public class User : BaseEntity
{

  [Key]
  [Column("id")]
  public long Id {get; set;}

  [Column("user_name")]
  public string UserName {get; set;} = string.Empty;

  [Column("password")]
  public string Password {get; set;} = string.Empty;

  [Column("full_name")]
  public string? FullName {get; set;}

  [Column("refresh_token")]
  public string? RefreshToken {get; set;}

  [Column("refresh_token_expiry_time")]
  public DateTime RefreshTokenExpiryTime {get; set;}
  
}