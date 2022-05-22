using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.auth;

[AddAnswerType(AnswerType.Token)]
public class TokenAnswer : EmptyAnswer
{
    public TokenType token_type { get; set; }
    public string access_toke { get; set; }

    public TokenAnswer(TokenType tokenType = default, string accessToke = null)
    {
        token_type = tokenType;
        access_toke = accessToke ?? throw new ArgumentNullException(nameof(accessToke));
    }
}