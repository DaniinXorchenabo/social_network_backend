using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.auth;

[AddAnswerType(AnswerType.Token)]
public record class TokenAnswer(
    TokenType token_type,
    string access_token

) : EmptyAnswer
{
}
