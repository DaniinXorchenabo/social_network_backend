using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.chat;

[AddAnswerType(AnswerType.chat)]
public record class ChatAnswer : BaseResponse<Chat>
{
    public ChatAnswer(params BaseResponsePart[] err_and_ans): base(err_and_ans){}
}

public record class ChatAnswerWithMessage : BaseResponse<ChatWithMessage>
{
    public ChatAnswerWithMessage(params BaseResponsePart[] err_and_ans): base(err_and_ans){}
}