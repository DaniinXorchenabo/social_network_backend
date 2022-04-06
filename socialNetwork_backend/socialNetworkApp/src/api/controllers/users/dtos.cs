using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.users;

[AddAnswerType(AnswerType.UserAnswer)]

public record class UserDto (
    Guid Id,
    string Username,
    string HashedPassword


): EmptyAnswer;
