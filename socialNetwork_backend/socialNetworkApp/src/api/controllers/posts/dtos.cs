using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.posts;

[AddAnswerType(AnswerType.Post)]
public record class PostDto(
    Guid Id,
    string Text,
    Guid? Author,
    DateTime CreatedAt,
    DateTime? UpdatedAt = null,
    int ViewedCount = 0,
    bool IsDeleted = false
) : EmptyAnswer;

public record CreatePostDto(
    string Text
    
) : EmptyAnswer;

public record UpdatePostDto(
    string Text
    
) : EmptyAnswer;