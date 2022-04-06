using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.posts;

[AddAnswerType(AnswerType.Post)]
public record class PostAnswer : BaseResponse<PostDto>
{
    public PostAnswer(params BaseResponsePart[] errAndAns): base(errAndAns){}
}
