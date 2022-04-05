using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.chat;

[AddAnswerType(AnswerType.chat)]
public record class Chat(
    Guid id,
    string name,
    DateTime createdAt,
    List<Guid> users,
    Guid? userCreator,
    Guid? groupCreator = null,
    ChatCreatorType chatCreatorType = ChatCreatorType.user,
    ChatType chatType = ChatType.simple,
    List<Guid>? admins = null,
    List<Guid>? blackList = null,
    string? photo = null,
    string? invitationUrl = null
) : EmptyAnswer
{
}

public record class ChatWithMessage(Guid id,
    string name,
    DateTime createdAt,
    List<Guid> users,
    Guid? userCreator,
    Message? message,
    Guid? groupCreator = null,
    ChatCreatorType chatCreatorType = ChatCreatorType.user,
    ChatType chatType = ChatType.simple,
    List<Guid>? admins = null,
    List<Guid>? blackList = null,
    string? photo = null,
    string? invitationUrl = null
) : EmptyAnswer;

public record class CreateChat(
    string name,
    List<Guid> users,
    Guid? userCreator,
    Guid? groupCreator = null,
    ChatCreatorType chatCreatorType = ChatCreatorType.user,
    ChatType chatType = ChatType.simple,
    string? photo = null
) : EmptyAnswer;

public record class UpdateChat(
    string name,
    ChatType chatType = ChatType.simple,
    string? photo = null
) : EmptyAnswer;