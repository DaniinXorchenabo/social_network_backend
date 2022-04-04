using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.responses;


public record BaseResponse<TAnswer> where TAnswer : EmptyAnswer
{
    public List<BaseAnswerRes<TAnswer>>? answers { get; set; }
    public List<EmptyError>? errors { get; set; }

    public BaseResponse(List<BaseAnswerRes<TAnswer>>? ans = null, List<EmptyError>? err = null)
    {
        this.answers = ans;
        this.errors = err;
    }
    public BaseResponse(List<TAnswer>? ans = null, List<EmptyError>? err = null)
    {
        AnswerType type = GetAnswerType();
        this.answers = ans?.Select(x => new BaseAnswerRes<TAnswer>(type, x)).ToList();
        this.errors = err;
    }
    public BaseResponse(List<EmptyError>? err = null, params TAnswer[] ans)
    {
        AnswerType type = GetAnswerType();
        this.answers = ans.Select(x => new BaseAnswerRes<TAnswer>(type, x)).ToList();
        this.errors = err;
    }
    public BaseResponse(params BaseResponsePart[] err_and_ans)
    {
        AnswerType type = GetAnswerType();
        this.answers = err_and_ans
            .Where(x => x is TAnswer)
            .Select(x => new BaseAnswerRes<TAnswer>(type, (x as TAnswer)!))
            .ToList();
        this.errors = err_and_ans
            .Where(x => x is EmptyError)
            .Select(x => (x as EmptyError)!)
            .ToList();
    }
    

    protected AnswerType GetAnswerType()
    {
        var attr = typeof(TAnswer)
            .GetCustomAttributes(typeof(AddAnswerTypeAttribute), true)
            .FirstOrDefault() as AddAnswerTypeAttribute;
        if (attr != null)
        {
            return attr!.Type;
        }
        throw new Exception($"Аттрибут не задан у класса {typeof(TAnswer).Name}");
        
    }
}



public record BaseResponsePart
{
    
}