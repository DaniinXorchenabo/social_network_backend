using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;


namespace socialNetworkApp.api.controllers.users;

[AddAnswerType(AnswerType.UserAnswer)]
public class UserAnswer<TValidate> : BaseResponse<GetUser, TValidate> where TValidate : AbstractDto
{
    public UserAnswer(params dynamic[] errAndAns) : base(errAndAns)
    {
    }
}

[AddAnswerType(AnswerType.UserAnswer)]
public class UserAnswer : UserAnswer<AbstractDto>
{
    public UserAnswer(params dynamic[] errAndAns) : base(errAndAns)
    {
    }
}