using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.chat;

[AddAnswerType(AnswerType.Chat)]
public class ChatDto : AbstractDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Guid> Users { get; set; }
    public Guid? UserCreator { get; set; }
    public Guid? GroupCreator { get; set; } = null;
    public ChatCreatorType ChatCreatorType { get; set; } = ChatCreatorType.User;
    public ChatType ChatType { get; set; } = ChatType.Simple;
    public List<Guid>? Admins { get; set; } = null;
    public List<Guid>? BlackList { get; set; } = null;
    public string? Photo { get; set; } = null;
    public string? InvitationUrl { get; set; } = null;

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
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Guid> Users { get; set; }
    public Guid? UserCreator { get; set; }
    public MessageDto? Message { get; set; }
    public Guid? GroupCreator { get; set; } = null;
    public ChatCreatorType ChatCreatorType { get; set; } = ChatCreatorType.User;
    public ChatType ChatType { get; set; } = ChatType.Simple;
    public List<Guid>? Admins { get; set; } = null;
    public List<Guid>? BlackList { get; set; } = null;
    public string? Photo { get; set; } = null;
    public string? InvitationUrl { get; set; } = null;

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
    public string Name { get; set; }

    public List<Guid> Users { get; set; }

    public ChatCreatorType ChatCreatorType { get; set; } = ChatCreatorType.User;
    public ChatType ChatType { get; set; } = ChatType.Simple;
    public string? Photo { get; set; } = null;

    public CreateChatDto(string name = null, List<Guid> users = null, Guid? userCreator = default,
        Guid? groupCreator = default, ChatCreatorType chatCreatorType = default, ChatType chatType = default,
        string? photo = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Users = users ?? throw new ArgumentNullException(nameof(users));
        ChatCreatorType = chatCreatorType;
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
    public string Name { get; set; }
    public ChatType ChatType { get; set; } = ChatType.Simple;
    public string? Photo { get; set; } = null;

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
    public UserOperationClass op { get; set; }
    public Guid[] users { get; set; }
}