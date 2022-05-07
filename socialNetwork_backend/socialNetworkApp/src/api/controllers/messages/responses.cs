using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.messages;

[AddAnswerType(AnswerType.Massage)]
public  class MessageAnswer : BaseResponse<MessageDto>
{
    public MessageAnswer(params BaseResponsePart[] errAndAns): base(errAndAns){}
}
