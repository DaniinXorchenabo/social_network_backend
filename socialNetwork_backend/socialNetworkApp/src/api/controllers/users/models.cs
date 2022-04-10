using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace socialNetworkApp.api.controllers.users;

[Table("users", Schema = "public")]
public class UserDb
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } 
    [Column("username")]
    public string Username { get; set; }= default!;
    [Column("hashed_password")]
    public string HashedPassword { get; set; } = default!;
}