using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.posts;

[AddAnswerType(AnswerType.Post)]
public  class PostAnswer : BaseResponse<PostDto>
{
    public PostAnswer(params dynamic[] errAndAns): base(errAndAns){}
}
