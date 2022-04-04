using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers;

[AddAnswerType(AnswerType.massage)]

public record class Message(
    Guid id,
    string text,
    Guid autor,
    Guid chat_id,
    DateTime created_at,
    DateTime? updated_at = null,
    MessageType messageType = MessageType.text,
    bool wathed = false,
    bool isDeleted = false
    ) : EmptyAnswer;