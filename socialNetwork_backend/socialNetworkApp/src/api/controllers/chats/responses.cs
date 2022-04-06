using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.chat;

[AddAnswerType(AnswerType.Chat)]
public record class ChatAnswer : BaseResponse<ChatDto>
{
    public ChatAnswer(params BaseResponsePart[] errAndAns): base(errAndAns){}
}

public record class ChatAnswerWithMessage : BaseResponse<ChatWithMessageDto>
{
    public ChatAnswerWithMessage(params BaseResponsePart[] errAndAns): base(errAndAns){}
}