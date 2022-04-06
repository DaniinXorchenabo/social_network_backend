using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.chat;

[AddAnswerType(AnswerType.Chat)]
public record class ChatDto(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    List<Guid> Users,
    Guid? UserCreator,
    Guid? GroupCreator = null,
    ChatCreatorType ChatCreatorType = ChatCreatorType.User,
    ChatType ChatType = ChatType.Simple,
    List<Guid>? Admins = null,
    List<Guid>? BlackList = null,
    string? Photo = null,
    string? InvitationUrl = null
) : EmptyAnswer
{
}

public record class ChatWithMessageDto(Guid Id,
    string Name,
    DateTime CreatedAt,
    List<Guid> Users,
    Guid? UserCreator,
    MessageDto? Message,
    Guid? GroupCreator = null,
    ChatCreatorType ChatCreatorType = ChatCreatorType.User,
    ChatType ChatType = ChatType.Simple,
    List<Guid>? Admins = null,
    List<Guid>? BlackList = null,
    string? Photo = null,
    string? InvitationUrl = null
) : EmptyAnswer;

public record class CreateChatDto(
    string Name,
    List<Guid> Users,
    Guid? UserCreator,
    Guid? GroupCreator = null,
    ChatCreatorType ChatCreatorType = ChatCreatorType.User,
    ChatType ChatType = ChatType.Simple,
    string? Photo = null
) : EmptyAnswer;

public record class UpdateChatDto(
    string Name,
    ChatType ChatType = ChatType.Simple,
    string? Photo = null
) : EmptyAnswer;