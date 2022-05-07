using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.db;

namespace socialNetworkApp.api.controllers.users;

[Table("users", Schema = "public")]
public class UserDb : AbstractEntity
{

    [Key] [Column("id")] public virtual Guid Id { get; set; }

    [Unicode()] [Column("username")] public virtual string Username { get; set; }

    [Unicode()][Column("email")] public virtual string Email { get; set; } 
    [Column("hashed_password")] public virtual string HashedPassword { get; set; }
    [Column("name")] public virtual string Name { get; set; }
    [Column("surname")] public virtual string Surname { get; set; }

    [Column("mods" )]// [Column("mods", TypeName = "integer[]")]
    public virtual List<AllModsEnum> Mods { get; set; } = new List<AllModsEnum>();

    // public virtual List<ModifiersOfAccessDb> Mods { get; set; } = new List<ModifiersOfAccessDb>();

    public UserDb(object obj) : base(obj)
    {
        
    }

    public UserDb() : base()
    {
        
    }
}