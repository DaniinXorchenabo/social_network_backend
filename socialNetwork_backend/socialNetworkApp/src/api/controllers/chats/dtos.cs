// using System.Text.Json.Serialization;

using System.ComponentModel.DataAnnotations;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.controllers.users;
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

    [Display(Name = "chat_creator_type")]
    public virtual ChatCreatorTypeEnum ChatCreatorTypeEnum { get; set; } = ChatCreatorTypeEnum.User;

    [Display(Name = "chat_type")] public virtual ChatTypeEnum ChatTypeEnum { get; set; } = ChatTypeEnum.Simple;
    // [Display(Name = "admins")] public virtual List<Guid>? Admins { get; set; } = null;
    // [Display(Name = "black_list")] public virtual List<Guid>? BlackList { get; set; } = null;
    [Display(Name = "photo")] public virtual string? Photo { get; set; } = null;
    [Display(Name = "invitation_url")] public virtual string? InvitationUrl { get; set; } = null;
    
    
    [Display(Name = "user_entities")]public List<UserDto> UserEntities { get; set; } = new List<UserDto>();
    [Display(Name = "chat_to_user_entities")] public List<ChatToUserDto> ChatUserEntities { get; set; } = new List<ChatToUserDto>();
    [Display(Name = "message_entities")] public List<MessageDto> MessageEntities { get; set; } =  new List<MessageDto>();

    public ChatDto(Guid id = default, string name = null, DateTime createdAt = default, List<Guid> users = null,
        Guid? userCreator = default, Guid? groupCreator = default, ChatCreatorTypeEnum chatCreatorTypeEnum = default,
        ChatTypeEnum chatTypeEnum = default, List<Guid>? admins = null, List<Guid>? blackList = null,
        string? photo = null,
        string? invitationUrl = null)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CreatedAt = createdAt;
        // Users = users ?? throw new ArgumentNullException(nameof(users));
        // UserCreator = userCreator;
        // GroupCreator = groupCreator;
        ChatCreatorTypeEnum = chatCreatorTypeEnum;
        ChatTypeEnum = chatTypeEnum;
        // Admins = admins;
        // BlackList = blackList;
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

    [Display(Name = "messages")] public virtual List<MessageDto>? Messages { get; set; }
    public List<ChatToUserDto> ChatUserEntities { get; set; } = new List<ChatToUserDto>();

    [Display(Name = "chat_creator_type")]
    // [JsonPropertyName("chat_creator_type")]
    public virtual ChatCreatorTypeEnum ChatCreatorTypeEnum { get; set; } = ChatCreatorTypeEnum.User;

    [Display(Name = "chat_type")] public virtual ChatTypeEnum ChatTypeEnum { get; set; } = ChatTypeEnum.Simple;

    [Display(Name = "photo")] public virtual string? Photo { get; set; } = null;
    [Display(Name = "invitation_url")] public virtual string? InvitationUrl { get; set; } = null;

    public ChatWithMessageDto(Guid id = default, string name = null, DateTime createdAt = default,
        List<Guid> users = null, Guid? userCreator = default, List<MessageDto>? messages = null,
        Guid? groupCreator = default,
        ChatCreatorTypeEnum chatCreatorTypeEnum = default, ChatTypeEnum chatTypeEnum = default,
        List<Guid>? admins = null,
        List<Guid>? blackList = null, string? photo = null, string? invitationUrl = null)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CreatedAt = createdAt;

        Messages = messages;

        ChatCreatorTypeEnum = chatCreatorTypeEnum;
        ChatTypeEnum = chatTypeEnum;

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

    [Required] [Display(Name = "users")] public virtual HashSet<Guid> Users { get; set; }

    [Display(Name = "chat__creator__type__1")]
    public virtual ChatCreatorTypeEnum ChatCreatorTypeEnumField { get; set; } = ChatCreatorTypeEnum.User;

    [Display(Name = "chat_type")] public virtual ChatTypeEnum ChatTypeEnum { get; set; } = ChatTypeEnum.Simple;
    [Display(Name = "photo")] public virtual string? Photo { get; set; } = null;

    public CreateChatDto(string name = null, HashSet<Guid> users = null, Guid? userCreator = default,
        Guid? groupCreator = default, ChatCreatorTypeEnum chatCreatorTypeEnumField = default,
        ChatTypeEnum chatTypeEnum = default,
        string? photo = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Users = users ?? throw new ArgumentNullException(nameof(users));
        ChatCreatorTypeEnumField = chatCreatorTypeEnumField;
        ChatTypeEnum = chatTypeEnum;
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
    [Display(Name = "chat_type")] public virtual ChatTypeEnum ChatTypeEnum { get; set; } = ChatTypeEnum.Simple;
    [Display(Name = "photo")] public virtual string? Photo { get; set; } = null;

    public UpdateChatDto(string name = null, ChatTypeEnum chatTypeEnum = default, string? photo = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        ChatTypeEnum = chatTypeEnum;
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
    public virtual UserOperationClassEnum op { get; set; }

    [Display(Name = "users")] public virtual HashSet<Guid> Users { get; set; }
}

[AddAnswerType(AnswerType.ChatToUserRntities)]
public class ChatToUserDto: AbstractDto
{
    [Display(Name = "chat_id")] public virtual Guid? ChatId { get; set; } = null;
    [Display(Name = "user_id")] public virtual Guid? UserId { get; set; } = null;

    [Display(Name = "roles")]
    public virtual List<ChatToUserRoleEnum>? Roles { get; set; } = null;

    // [Display(Name = "chat_entities")] public ChatDto? ChatEntity { get; set; } = null;
    [Display(Name = "user_entities")] public UserDto? UserEntity { get; set; } = null;
    // [Display(Name = "message_entities")] public List<MessageDto>? MessageEntities { get; set; } = null;
    
    
    public ChatToUserDto(object obj) : base(obj)
    {
    }

    public ChatToUserDto()
    {
    }
}