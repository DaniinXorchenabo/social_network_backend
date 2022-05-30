using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.api.controllers.users;
using socialNetworkApp.db;

namespace socialNetworkApp.api.controllers.chat;

[Table("chats", Schema = "public")]
public class ChatDb : AbstractEntity
{
    
    [Key] [Column("id")] public Guid Id { get; set; }
    [Column("name")] public string Name { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; set; }

    [Column("chat_creator_type")] public ChatCreatorTypeEnum ChatCreatorTypeEnum { get; set; } = ChatCreatorTypeEnum.User;

    [Column("chat_type")] public ChatTypeEnum ChatTypeEnum { get; set; } = ChatTypeEnum.Simple;

    [Column("image")] public string? Photo { get; set; } = null;
    [Column("invitation_url")] public string? InvitationUrl { get; set; } = null;

    [NotMapped]
    public List<ChatToUserDb> ChatUserEntities { get; set; } = new List<ChatToUserDb>();
    [NotMapped]
    public List<MessageDb> MessageEntities { get; set; } =  new List<MessageDb>();


    public ChatDb(object obj) : base(obj)
    {
    }

    public ChatDb() : base()
    {
    }
}


public class ChatsAndMessageTableDb : ChatDb
{
    [Key][Column("id")] public  Guid Id { get; set; }
    [ForeignKey("user_id_in_msg__fk")]
    [Column("author_id")] public  Guid? AuthorId { get; set; }
    [ForeignKey("chat_id_in_msg__fk")]
    [Column("chat_id")] public  Guid ChatId { get; set; }
    
    [NotMapped]
    public UserDb AuthorEntity{ get; set; }
    [NotMapped]
    public ChatDb ChatEntity{ get; set; }
    [NotMapped]
    public ChatToUserDb ChatsAndUsersEntity{ get; set; }
    
    [Column("text")] public  string Text { get; set; }
    
    [Column("created_at")]
    public  DateTime CreatedAt { get; set; }
    [Column("updated_at")] 
    public  DateTime? UpdatedAt { get; set; } = null;
    
    [Column("message_type")]
    public  MessageTypeEnum MessageTypeEnum { get; set; } = MessageTypeEnum.Text;
    
    [Column("is_viewed")] public  bool Viewed { get; set; } = false;
    [Column("is_deleted")] public  bool IsDeleted { get; set; } = false;
}


[Table("chats_to_users", Schema = "public")]
public class ChatToUserDb : AbstractEntity
{
    [ForeignKey("chat_id__fk")][Column("chat_id")] public virtual Guid ChatId { get; set; }
    [ForeignKey("user_id__fk")][Column("user_id")] public virtual Guid UserId { get; set; }
    [Column("roles")] public virtual List<ChatToUserRoleEnum> Roles { get; set; } = new() {ChatToUserRoleEnum.User};

    [NotMapped] public ChatDb ChatEntity { get; set; }
    [NotMapped] public UserDb UserEntity { get; set; }
    [NotMapped][Column("message_entities")]public List<MessageDb> MessageEntities { get; set; } =  new List<MessageDb>();


    public ChatToUserDb(object obj) : base(obj)
    {
    }

    public ChatToUserDb() : base()
    {
    }
}