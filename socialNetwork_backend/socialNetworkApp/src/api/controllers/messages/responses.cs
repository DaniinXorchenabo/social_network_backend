using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.messages;

[AddAnswerType(AnswerType.massage)]
public record class MessageAnswer : BaseResponse<Message>
{
    public MessageAnswer(params BaseResponsePart[] err_and_ans): base(err_and_ans){}
}
