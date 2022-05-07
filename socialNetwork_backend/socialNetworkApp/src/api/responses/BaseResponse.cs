using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.responses;


public class BaseResponse<TAnswer> where TAnswer : EmptyAnswer
{
    public List<BaseAnswerRes<TAnswer>>? Answers { get; set; }
    public List<EmptyError>? Errors { get; set; }

    public BaseResponse(List<BaseAnswerRes<TAnswer>>? ans = null, List<EmptyError>? err = null)
    {
        this.Answers = ans;
        this.Errors = err;

    }
    public BaseResponse(List<TAnswer>? ans = null, List<EmptyError>? err = null)
    {
        AnswerType type = GetAnswerType();
        this.Answers = ans?.Select(x => new BaseAnswerRes<TAnswer>(type, x)).ToList();
        this.Errors = err;
    }
    public BaseResponse(List<EmptyError>? err = null, params TAnswer[] ans)
    {
        AnswerType type = GetAnswerType();
        this.Answers = ans.Select(x => new BaseAnswerRes<TAnswer>(type, x)).ToList();
        this.Errors = err;
    }
    public BaseResponse(params BaseResponsePart[] errAndAns)
    {
        AnswerType type = GetAnswerType();
        this.Answers = errAndAns
            .Where(x => x is TAnswer)
            .Select(x => new BaseAnswerRes<TAnswer>(type, (x as TAnswer)!))
            .ToList();
        this.Errors = errAndAns
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

    public Task ExecuteResultAsync(ActionContext context)
    {
        // OkObjectResult
        throw new NotImplementedException();
    }

    public int? StatusCode { get; }
}


public class Resp : ObjectResult, IActionResult, IStatusCodeActionResult
{
    public Resp(int statusCode, object? value) : base(value)
    {
        StatusCode = statusCode;
    }
}

public record class BaseResponsePart
{
}