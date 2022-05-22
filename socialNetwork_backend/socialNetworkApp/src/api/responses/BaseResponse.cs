using System.Collections;
using System.ComponentModel;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses.utils;
using socialNetworkApp.db;

namespace socialNetworkApp.api.responses;

public abstract class BaseResponse
{
}

public interface IBaseResponse<out TAnswer, out TValidate, out TError>
{
}

public interface IBaseResponse<out TAnswer, out TValidate> : IBaseResponse<TAnswer, TValidate, EmptyError>
{
}

public interface IBaseResponse<out TAnswer> : IBaseResponse<TAnswer, AbstractDto, EmptyError>
{
}

public class BaseResponse<TAnswer, TValidate, TError> : BaseResponse, IBaseResponse<TAnswer, TValidate, TError>
    where TAnswer : EmptyAnswer
    where TValidate : AbstractDto
    where TError : EmptyError
{
    public List<BaseAnswerRes<TAnswer>>? Answers { get; set; }
    public List<TError>? Errors { get; set; }

    public BaseResponse(params dynamic[] values)
    {
        this.Errors = new List<TError>();
        this.Answers = new List<BaseAnswerRes<TAnswer>>();

        oneParamProcessing(values);


        if (this.Errors.Count == 0)
        {
            this.Errors = null;
        }

        if (this.Answers.Count == 0)
        {
            this.Answers = null;
        }
    }

    public void oneParamProcessing(dynamic value)
    {
        if (value is IEnumerable valueEnumerator)
        {
            foreach (var o in valueEnumerator)
            {
                oneParamProcessing(o);
            }
        }
        else
        {
            if (value is BaseResponsePart valueAsBaseResponsePart)
            {
                if (valueAsBaseResponsePart is TAnswer valueAsTAns)
                {
                    this.Answers.Add(new BaseAnswerRes<TAnswer>(GetAnswerType(), valueAsTAns));
                }
                else if (valueAsBaseResponsePart is TError valueAsTErr)
                {
                    this.Errors.Add(valueAsTErr);
                }
            }
            else if (value is TError[] valueAsTErrorArray)
            {
                this.Errors.AddRange(valueAsTErrorArray.ToList());
            }
            else if (value is TAnswer valueAsTAnswer)
            {
                this.Answers.Add(new BaseAnswerRes<TAnswer>(GetAnswerType(), valueAsTAnswer));
            }
            else if (value is BaseAnswerRes<TAnswer> valueAsBaseRTAnswer)
            {
                this.Answers.Add(valueAsBaseRTAnswer);
            }
            else if (value is AbstractEntity valueAsAbstractEntity)
            {
                Type dtoType = this.GetType()
                    .GetProperty("Answers")
                    .PropertyType
                    .GenericTypeArguments[0]
                    .GenericTypeArguments[0];
                TAnswer answerFromDbModel = Activator.CreateInstance(dtoType, valueAsAbstractEntity) as TAnswer;
                this.Answers.Add(new BaseAnswerRes<TAnswer>(GetAnswerType(), answerFromDbModel));
            }
        }
    }

    public BaseResponse()
    {
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
}

public class BaseResponse<TAnswer, TValidate>
    : BaseResponse<TAnswer, TValidate, EmptyError>,
        IBaseResponse<TAnswer, TValidate>
    where TAnswer : EmptyAnswer
    where TValidate : AbstractDto
{
    public BaseResponse(params dynamic[] values) : base(values)
    {
    }

    protected BaseResponse()
    {
    }
}

public class BaseResponse<TAnswer> :
    BaseResponse<TAnswer, AbstractDto, EmptyError>,
    IBaseResponse<TAnswer>
    where TAnswer : EmptyAnswer
{
    public BaseResponse(params dynamic[] values) : base(values)
    {
    }

    protected BaseResponse()
    {
    }
}

public class Resp : ObjectResult, IActionResult, IStatusCodeActionResult
{
    public Resp(int statusCode, object? value) : base(value)
    {
        StatusCode = statusCode;
    }
}

public class BaseResponsePart
{
}