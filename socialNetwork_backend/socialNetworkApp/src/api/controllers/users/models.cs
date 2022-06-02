using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.api.controllers.chat;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.db;
using ChatToUserDb = socialNetworkApp.api.controllers.chat.ChatToUserDb;

namespace socialNetworkApp.api.controllers.users;

[Table("users", Schema = "public")]
[Microsoft.EntityFrameworkCore.Index(nameof(Email), Name = "Index__UniqueEmail", IsUnique = true)]
[Microsoft.EntityFrameworkCore.Index(nameof(Username), Name = "Index__UniqueUsername", IsUnique = true)]
public class UserDb : AbstractEntity
{
    // [Comment("The URL of the blog")]
    [Key] [Required] [Column("id")] public virtual Guid Id { get; set; }

    [Unicode]
    [Required]
    [Column("username")]
    public virtual string Username { get; set; }

    [Unicode] [Required] [Column("email")] public virtual string Email { get; set; }
    [Required] [Column("hashed_password")] public virtual string HashedPassword { get; set; }
    [Required] [Column("name")] public virtual string Name { get; set; }

    [Required] [Column("surname")] public virtual string Surname { get; set; }
    [Required] [Column("is_deleted")] public virtual bool IsDeleted { get; set; } = false;

    [Required]
    [Column("mods")] // [Column("mods", TypeName = "integer[]")]
    public virtual List<AllModsEnum> Mods { get; set; } = new List<AllModsEnum>();
    
    [NotMapped] public List<ChatDb> ChatEntities { get; set; } =  new List<ChatDb>();
    [NotMapped] public List<ChatToUserDb> ChatUserEntities { get; set; } =  new List<ChatToUserDb>();
    [NotMapped] public List<MessageDb> MessageEntities { get; set; } =  new List<MessageDb>();
    


    public UserDb(object obj) : base(obj)
    {
    }

    public UserDb() : base()
    {
    }
}