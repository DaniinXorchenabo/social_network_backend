﻿using System.ComponentModel.DataAnnotations;
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

    [Column("chat_creator_type", TypeName = "chat_creator_type")] public ChatCreatorType ChatCreatorType { get; set; } = ChatCreatorType.User;

    [Column("chat_type", TypeName = "chat_type")] public ChatType ChatType { get; set; } = ChatType.Simple;

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

[Table("chats_to_users", Schema = "public")]
public class ChatToUserDb : AbstractEntity
{
    [ForeignKey("chat_id__fk")][Column("chat_id")] public virtual Guid ChatId { get; set; }
    [ForeignKey("user_id__fk")][Column("user_id")] public virtual Guid UserId { get; set; }
    [Column("roles", TypeName = "chat_to_user_role[]")] public virtual List<ChatToUserRole> Roles { get; set; } = new() {ChatToUserRole.User};

    [NotMapped]
    public ChatDb ChatEntity { get; set; }
    
    [NotMapped]
    public UserDb UserEntity { get; set; }
    [NotMapped]
    public List<MessageDb> MessageEntities { get; set; } =  new List<MessageDb>();


    public ChatToUserDb(object obj) : base(obj)
    {
    }

    public ChatToUserDb() : base()
    {
    }
}