using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.db;

namespace socialNetworkApp.api.controllers.users;

[Table("users", Schema = "public")]
[Index(nameof(Email), Name = "Index__UniqueEmail", IsUnique = true)]
[Index(nameof(Username), Name = "Index__UniqueUsername", IsUnique = true)]
public class UserDb : AbstractEntity
{
    // [Comment("The URL of the blog")]
    [Key][Required] [Column("id")] public virtual Guid Id { get; set; }

    [Unicode][Required][Column("username")] public virtual string Username { get; set; }

    [Unicode][Required][Column("email")] public virtual string Email { get; set; } 
    [Required][Column("hashed_password")] public virtual string HashedPassword { get; set; }
    [Required][Column("name")] public virtual string Name { get; set; }
    [Required][Column("surname")] public virtual string Surname { get; set; }

    [Required] [Column("mods" )]// [Column("mods", TypeName = "integer[]")]
    public virtual List<AllModsEnum> Mods { get; set; } = new List<AllModsEnum>();

    // public virtual List<ModifiersOfAccessDb> Mods { get; set; } = new List<ModifiersOfAccessDb>();

    public UserDb(object obj) : base(obj)
    {
        
    }

    public UserDb() : base()
    {
        
    }
}