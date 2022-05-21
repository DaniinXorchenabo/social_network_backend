using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;


namespace socialNetworkApp.api.controllers.chat;


[AddAnswerType(AnswerType.Chat)]
public  class ChatAnswer<TValidate> : BaseResponse<ChatDto, TValidate> where TValidate: AbstractDto
{
    public ChatAnswer(params dynamic[] errAndAns): base(errAndAns){}
}


[AddAnswerType(AnswerType.Chat)]
public class ChatAnswer : ChatAnswer<AbstractDto>
{
    public ChatAnswer(params dynamic[] errAndAns) : base(errAndAns)
    {
    }
}


[AddAnswerType(AnswerType.Chat)]
public  class ChatAnswerWithMessage<TValidate> : BaseResponse<ChatWithMessageDto, TValidate>
    where TValidate: AbstractDto
{
    public ChatAnswerWithMessage(params dynamic[] errAndAns): base(errAndAns){}
}


[AddAnswerType(AnswerType.Chat)]
public  class ChatAnswerWithMessage : ChatAnswerWithMessage<AbstractDto>
{
    public ChatAnswerWithMessage(params dynamic[] errAndAns): base(errAndAns){}
}