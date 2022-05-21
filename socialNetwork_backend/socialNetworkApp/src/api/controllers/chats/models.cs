

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.db;

namespace socialNetworkApp.api.controllers.chat;

[Table("chats", Schema = "public")]
public class ChatDb : AbstractEntity
{
    [Key] [Column("id")] public Guid Id{ get; set; }
    [Column("name")] public string Name{ get; set; }
    [Column("created_at")] public DateTime CreatedAt{ get; set; }
    // public List<Guid> Users{ get; set; };
    [Column("chat_creator_type")] public ChatCreatorType ChatCreatorType{ get; set; } = ChatCreatorType.User;
    [Column("chat_type")] public ChatType ChatType{ get; set; } = ChatType.Simple;
    // public List<Guid>? Admins{ get; set; } = null;
    // public List<Guid>? BlackList{ get; set; } = null;
    [Column("image")] public string? Photo{ get; set; } = null;
    [Column("invitation_url")] public string? InvitationUrl{ get; set; } = null;

    // public virtual List<ModifiersOfAccessDb> Mods { get; set; } = new List<ModifiersOfAccessDb>();

    public ChatDb(object obj) : base(obj)
    {
        
    }

    public ChatDb() : base()
    {
        
    }
}


[Table("chats_to_users", Schema = "public")]
public class ChatToUserDb : AbstractEntity
{

    [Key] [Column("chat_id")] public virtual Guid ChatId { get; set; }
    [Key] [Column("user_id")] public virtual Guid UserId { get; set; }
    [Column("roles")] public virtual ChatToUserRole[] Roles { get; set; } = new[] {ChatToUserRole.User};


    public ChatToUserDb(object obj) : base(obj)
    {
        
    }

    public ChatToUserDb() : base()
    {
        
    }
}
