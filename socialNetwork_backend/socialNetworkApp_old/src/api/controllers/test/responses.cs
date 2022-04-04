using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers;

[AddAnswerType(AnswerType.pointAnswer)]
public record class PointAnswer : BaseResponse<Point>
{
    public PointAnswer(params BaseResponsePart[] err_and_ans): base(err_and_ans){}
}
