using socialNetworkApp.api.controllers.chat;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.messages;

[AddAnswerType(AnswerType.Massage)]
public  class MessageAnswer<TValidate> : BaseResponse<MessageDto, TValidate> where TValidate: AbstractDto
{
    public MessageAnswer(params dynamic[] errAndAns): base(errAndAns){}
}

[AddAnswerType(AnswerType.Massage)]
public  class MessageAnswer : MessageAnswer<AbstractDto>
{
    public MessageAnswer(params dynamic[] errAndAns): base(errAndAns){}
}
