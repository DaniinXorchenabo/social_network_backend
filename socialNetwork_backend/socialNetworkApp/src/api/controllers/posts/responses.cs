using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.posts;

[AddAnswerType(AnswerType.post)]
public record class PostAnswer : BaseResponse<Post>
{
    public PostAnswer(params BaseResponsePart[] err_and_ans): base(err_and_ans){}
}
