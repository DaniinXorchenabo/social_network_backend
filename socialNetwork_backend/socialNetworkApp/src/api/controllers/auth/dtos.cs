using System.ComponentModel.DataAnnotations;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.auth;

[AddAnswerType(AnswerType.Token)]
public class TokenAnswer : EmptyAnswer
{
    [Required]
    [Display(Name = "token_type")]
    public virtual TokenTypeEnum TokenTypeEnum { get; set; }

    [Required]
    [Display(Name = "access_toke")]
    public virtual string AccessToke { get; set; }

    public TokenAnswer(TokenTypeEnum tokenTypeEnum = default, string accessToke = null)
    {
        TokenTypeEnum = tokenTypeEnum;
        AccessToke = accessToke ?? throw new ArgumentNullException(nameof(accessToke));
    }
}