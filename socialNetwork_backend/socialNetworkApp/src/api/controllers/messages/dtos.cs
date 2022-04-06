using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.messages;

[AddAnswerType(AnswerType.Massage)]
public record class MessageDto(
    Guid Id,
    string Text,
    Guid? Autor,
    Guid ChatId,
    DateTime CreatedAt,
    DateTime? UpdatedAt = null,
    MessageType MessageType = MessageType.Text,
    bool Viewed = false,
    bool IsDeleted = false
) : EmptyAnswer;

public record CreateMessageDto(
    string Text,
    Guid Author,
    MessageType MessageType = MessageType.Text
) : EmptyAnswer;