using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.posts;

[AddAnswerType(AnswerType.Post)]
public  class PostAnswer<TValidate> : BaseResponse<PostDto, TValidate> where TValidate: AbstractDto
{
    public PostAnswer(params dynamic[] errAndAns): base(errAndAns){}
}


[AddAnswerType(AnswerType.Post)]
public  class PostAnswer : PostAnswer<AbstractDto>
{
    public PostAnswer(params dynamic[] errAndAns): base(errAndAns){}
}
