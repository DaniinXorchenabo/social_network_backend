using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.posts;

[AddAnswerType(AnswerType.post)]
public record class Post(
    Guid id,
    string text,
    Guid? autor,
    DateTime createdAt,
    DateTime? updatedAt = null,
    int wathedConut = 0,
    bool isDeleted = false
) : EmptyAnswer;

public record CreatePost(
    string text
    
) : EmptyAnswer;

public record UpdatePost(
    string text
    
) : EmptyAnswer;