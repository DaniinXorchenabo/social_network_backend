using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.chat;

[AddAnswerType(AnswerType.Chat)]
public class ChatDto : AbstractDto
{
    [Required] [Display(Name = "id")] public virtual Guid Id { get; set; }
    [Required] [Display(Name = "name")] public virtual string Name { get; set; }

    [Required]
    [Display(Name = "created_at")]
    public virtual DateTime CreatedAt { get; set; }

    [Required] [Display(Name = "users")] public virtual List<Guid> Users { get; set; }
    [Display(Name = "user_creator")] public virtual Guid? UserCreator { get; set; }
    [Display(Name = "group_creator")] public virtual Guid? GroupCreator { get; set; } = null;

    [Display(Name = "chat_creator_type")]
    public virtual ChatCreatorType ChatCreatorType { get; set; } = ChatCreatorType.User;

    [Display(Name = "chat_type")] public virtual ChatType ChatType { get; set; } = ChatType.Simple;
    [Display(Name = "admins")] public virtual List<Guid>? Admins { get; set; } = null;
    [Display(Name = "black_list")] public virtual List<Guid>? BlackList { get; set; } = null;
    [Display(Name = "photo")] public virtual string? Photo { get; set; } = null;
    [Display(Name = "invitation_url")] public virtual string? InvitationUrl { get; set; } = null;

    public ChatDto(Guid id = default, string name = null, DateTime createdAt = default, List<Guid> users = null,
        Guid? userCreator = default, Guid? groupCreator = default, ChatCreatorType chatCreatorType = default,
        ChatType chatType = default, List<Guid>? admins = null, List<Guid>? blackList = null, string? photo = null,
        string? invitationUrl = null)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CreatedAt = createdAt;
        Users = users ?? throw new ArgumentNullException(nameof(users));
        UserCreator = userCreator;
        GroupCreator = groupCreator;
        ChatCreatorType = chatCreatorType;
        ChatType = chatType;
        Admins = admins;
        BlackList = blackList;
        Photo = photo;
        InvitationUrl = invitationUrl;
    }

    public ChatDto(object obj) : base(obj)
    {
    }

    public ChatDto()
    {
    }
}

[AddAnswerType(AnswerType.Chat)]
public class ChatWithMessageDto : AbstractDto
{
    [Required] [Display(Name = "id")] public virtual Guid Id { get; set; }
    [Required] [Display(Name = "name")] public virtual string Name { get; set; }

    [Required]
    [Display(Name = "created_at")]
    public virtual DateTime CreatedAt { get; set; }

    [Required] [Display(Name = "users")] public virtual List<Guid> Users { get; set; }
    [Display(Name = "user_creator")] public virtual Guid? UserCreator { get; set; }
    [Display(Name = "message")] public virtual MessageDto? Message { get; set; }
    [Display(Name = "group_creator")] public virtual Guid? GroupCreator { get; set; } = null;

    [Display(Name = "chat_creator_type")]
    [JsonPropertyName("chat_creator_type")]
    public virtual ChatCreatorType ChatCreatorType { get; set; } = ChatCreatorType.User;

    [Display(Name = "chat_type")] public virtual ChatType ChatType { get; set; } = ChatType.Simple;
    [Display(Name = "admins")] public virtual List<Guid>? Admins { get; set; } = null;
    [Display(Name = "black_list")] public virtual List<Guid>? BlackList { get; set; } = null;
    [Display(Name = "photo")] public virtual string? Photo { get; set; } = null;
    [Display(Name = "invitation_url")] public virtual string? InvitationUrl { get; set; } = null;

    public ChatWithMessageDto(Guid id = default, string name = null, DateTime createdAt = default,
        List<Guid> users = null, Guid? userCreator = default, MessageDto? message = null, Guid? groupCreator = default,
        ChatCreatorType chatCreatorType = default, ChatType chatType = default, List<Guid>? admins = null,
        List<Guid>? blackList = null, string? photo = null, string? invitationUrl = null)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CreatedAt = createdAt;
        Users = users ?? throw new ArgumentNullException(nameof(users));
        UserCreator = userCreator;
        Message = message;
        GroupCreator = groupCreator;
        ChatCreatorType = chatCreatorType;
        ChatType = chatType;
        Admins = admins;
        BlackList = blackList;
        Photo = photo;
        InvitationUrl = invitationUrl;
    }

    public ChatWithMessageDto(object obj) : base(obj)
    {
    }

    public ChatWithMessageDto()
    {
    }
}

[AddAnswerType(AnswerType.Chat)]
public class CreateChatDto : AbstractDto
{
    // [Obsolete]
    [Required] [Display(Name = "name")] public virtual string Name { get; set; }

    [Required] [Display(Name = "users")] public virtual List<Guid> Users { get; set; }

    [Display(Name = "chat__creator__type__1")]
    public virtual ChatCreatorType ChatCreatorTypeField { get; set; } = ChatCreatorType.User;

    [Display(Name = "chat_type")] public virtual ChatType ChatType { get; set; } = ChatType.Simple;
    [Display(Name = "photo")] public virtual string? Photo { get; set; } = null;

    public CreateChatDto(string name = null, List<Guid> users = null, Guid? userCreator = default,
        Guid? groupCreator = default, ChatCreatorType chatCreatorTypeField = default, ChatType chatType = default,
        string? photo = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Users = users ?? throw new ArgumentNullException(nameof(users));
        ChatCreatorTypeField = chatCreatorTypeField;
        ChatType = chatType;
        Photo = photo;
    }

    public CreateChatDto(object obj) : base(obj)
    {
    }

    public CreateChatDto()
    {
    }
}

[AddAnswerType(AnswerType.Chat)]
public class UpdateChatDto : AbstractDto
{
    [Display(Name = "name")] public virtual string? Name { get; set; }
    [Display(Name = "chat_type")] public virtual ChatType ChatType { get; set; } = ChatType.Simple;
    [Display(Name = "photo")] public virtual string? Photo { get; set; } = null;

    public UpdateChatDto(string name = null, ChatType chatType = default, string? photo = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        ChatType = chatType;
        Photo = photo;
    }

    public UpdateChatDto(object obj) : base(obj)
    {
    }

    public UpdateChatDto()
    {
    }
}

[AddAnswerType(AnswerType.Chat)]
public class UpdateChatUsersDto : AbstractDto
{
    [Display(Name = "user_operation_class")]
    public virtual UserOperationClass op { get; set; }

    [Display(Name = "users")] public virtual Guid[] users { get; set; }
}