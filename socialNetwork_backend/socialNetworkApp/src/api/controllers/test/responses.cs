using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.test;

[AddAnswerType(AnswerType.PointAnswer)]
public record class PointAnswer : BaseResponse<Point>
{
    public PointAnswer(params BaseResponsePart[] errAndAns): base(errAndAns){}
}
