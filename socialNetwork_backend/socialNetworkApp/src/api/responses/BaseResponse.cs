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

    // public BaseResponse(List<BaseAnswerRes<TAnswer>>? ans = null, List<TError>? err = null)
    // {
    //     this.Answers = ans;
    //     this.Errors = err;
    // }
    //
    // public BaseResponse(List<TAnswer>? ans = null, List<TError>? err = null)
    // {
    //     AnswerType type = GetAnswerType();
    //     this.Answers = ans?.Select(x => new BaseAnswerRes<TAnswer>(type, x)).ToList();
    //     this.Errors = err;
    // }
    //
    // public BaseResponse(List<TError>? err = null, params TAnswer[] ans)
    // {
    //     AnswerType type = GetAnswerType();
    //     this.Answers = ans.Select(x => new BaseAnswerRes<TAnswer>(type, x)).ToList();
    //     this.Errors = err;
    // }
    //
    // public BaseResponse(params BaseResponsePart[] errAndAns)
    // {
    //     AnswerType type = GetAnswerType();
    //     this.Answers = errAndAns
    //         .Where(x => x is TAnswer)
    //         .Select(x => new BaseAnswerRes<TAnswer>(type, (x as TAnswer)!))
    //         .ToList();
    //     this.Errors = errAndAns
    //         .Where(x => x is TError)
    //         .Select(x => (x as TError)!)
    //         .ToList();
    // }
    //
    // public BaseResponse(List<BaseResponsePart> errAndAns)
    // {
    //     AnswerType type = GetAnswerType();
    //     this.Answers = errAndAns
    //         .Where(x => x is TAnswer)
    //         .Select(x => new BaseAnswerRes<TAnswer>(type, (x as TAnswer)!))
    //         .ToList();
    //     this.Errors = errAndAns
    //         .Where(x => x is TError)
    //         .Select(x => (x as TError)!)
    //         .ToList();
    // }

    // public static TContainsType? CheckGenericTypeFunc<TContainsType>(dynamic val)
    // {
    //     if (val is TContainsType resultValue)
    //     {
    //         return resultValue;
    //     }
    //     if (val.GetType().IsGenericType)
    //     {
    //         var genericContainType = typeof(TContainsType).GetGenericTypeDefinition();
    //         if (val.GetType().GetGenericTypeDefinition() == genericContainType)
    //         {
    //             var targetValueType = typeof(TContainsType).GenericTypeArguments[0];
    //
    //             if ((val.GetType() as Type).GenericTypeArguments[0].IsSubclassOf(targetValueType))
    //             {
    //                 return val;
    //             }
    //         }
    //
    //     }
    //
    //     return null;
    // }

    public BaseResponse(params dynamic[] values)
    {
        this.Errors = new List<TError>();
        this.Answers = new List<BaseAnswerRes<TAnswer>>();

        oneParamProcessing(values);
        
        // foreach (var value in values)
        // {
        //
        // var testFunc = (Type checkedVal, dynamic val, Type genericContainType) => 

            // if (value is List<BaseResponsePart> valueAsList)
            // {
            //     AnswerType type = GetAnswerType();
            //     this.Answers.AddRange(valueAsList
            //         .Where(x => x is TAnswer)
            //         .Select(x => new BaseAnswerRes<TAnswer>(type, (x as TAnswer)!))
            //         .ToList());
            //     this.Errors.AddRange(valueAsList
            //         .Where(x => x is TError)
            //         .Select(x => (x as TError)!)
            //         .ToList());
            // }
            // else if (value is BaseResponsePart[] valueAsArray)
            // {
            //     AnswerType type = GetAnswerType();
            //     this.Answers.AddRange(valueAsArray
            //         .Where(x => x is TAnswer)
            //         .Select(x => new BaseAnswerRes<TAnswer>(type, (x as TAnswer)!))
            //         .ToList());
            //     this.Errors.AddRange(valueAsArray
            //         .Where(x => x is TError)
            //         .Select(x => (x as TError)!)
            //         .ToList());
            // }
            // else if (value is BaseResponsePart valueAsBaseResponsePart)
            // {
            //     if (valueAsBaseResponsePart is TAnswer valueAsTAns)
            //     {
            //         this.Answers.Add(new BaseAnswerRes<TAnswer>(GetAnswerType(), valueAsTAns));
            //     }
            //     else if (valueAsBaseResponsePart is TError valueAsTErr)
            //     {
            //         this.Errors.Add(valueAsTErr);
            //     }
            // }
            // else if (value is List<TError> valueAsTErrorList)
            // {
            //     this.Errors.AddRange(valueAsTErrorList);
            // }
            // else if (value is TError[] valueAsTErrorArray)
            // {
            //     this.Errors.AddRange(valueAsTErrorArray.ToList());
            // }
            // else if (value is TError valueAsTError)
            // {
            //     this.Errors.Add(valueAsTError);
            // }
            // else if (value is List<TAnswer> valueAsTAnswerList)
            // {
            //     AnswerType type = GetAnswerType();
            //     this.Answers.AddRange(valueAsTAnswerList
            //         .Where(x => x is TAnswer)
            //         .Select(x => new BaseAnswerRes<TAnswer>(type, (x as TAnswer)!))
            //         .ToList());
            // }
            // else if (value is TAnswer[] valueAsTAnswerArray)
            // {
            //     AnswerType type = GetAnswerType();
            //     this.Answers.AddRange(valueAsTAnswerArray
            //         .Where(x => x is TAnswer)
            //         .Select(x => new BaseAnswerRes<TAnswer>(type, (x as TAnswer)!))
            //         .ToList());
            // }
            // else if (value is TAnswer valueAsTAnswer)
            // {
            //     this.Answers.Add(new BaseAnswerRes<TAnswer>(GetAnswerType(), valueAsTAnswer));
            // }
            // else if (value is List<BaseAnswerRes<TAnswer>> valueAsBaseRTAnswerList)
            // {
            //     this.Answers.AddRange(valueAsBaseRTAnswerList);
            // }
            // else if (value is BaseAnswerRes<TAnswer>[] valueAsBaseRTAnswerArray)
            // {
            //     this.Answers.AddRange(valueAsBaseRTAnswerArray.ToList());
            // }
            // else if (value is BaseAnswerRes<TAnswer> valueAsBaseRTAnswer)
            // {
            //     this.Answers.Add(valueAsBaseRTAnswer);
            // }
            // else if (value is List<AbstractEntity> valueAsAbstractEntityList)
            // {
            //     Type dtoType = this.GetType()
            //         .GetProperty("Answers")
            //         .PropertyType
            //         .GenericTypeArguments[0]
            //         .GenericTypeArguments[0];
            //     this.Answers.AddRange(
            //         valueAsAbstractEntityList
            //             .Select(x => (Activator.CreateInstance(dtoType, new object[] {x}) as TAnswer))
            //             .Select(x => new BaseAnswerRes<TAnswer>(GetAnswerType(), x)).ToList());
            // }
            // else if (value is AbstractEntity[] valueAsAbstractEntityArray)
            // {
            //     Type dtoType = this.GetType()
            //         .GetProperty("Answers")
            //         .PropertyType
            //         .GenericTypeArguments[0]
            //         .GenericTypeArguments[0];
            //     this.Answers.AddRange(
            //         valueAsAbstractEntityArray
            //             .Select(x => (Activator.CreateInstance(dtoType, new object[] {x}) as TAnswer))
            //             .Select(x => new BaseAnswerRes<TAnswer>(GetAnswerType(), x)).ToList());
            // }
            // else if (value is AbstractEntity valueAsAbstractEntity)
            // {
            //     Type dtoType = this.GetType()
            //         .GetProperty("Answers")
            //         .PropertyType
            //         .GenericTypeArguments[0]
            //         .GenericTypeArguments[0];
            //     TAnswer answerFromDbModel =
            //         (Activator.CreateInstance(dtoType, new object[] {valueAsAbstractEntity}) as TAnswer);
            //     this.Answers.Add(new BaseAnswerRes<TAnswer>(GetAnswerType(), answerFromDbModel));
            // }
        // }

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
                TAnswer answerFromDbModel = Activator.CreateInstance(dtoType,  valueAsAbstractEntity) as TAnswer;
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
    // public BaseResponse(List<BaseAnswerRes<TAnswer>>? ans = null, List<EmptyError>? err = null) : base(ans, err)
    // {
    // }
    //
    // public BaseResponse(List<TAnswer>? ans = null, List<EmptyError>? err = null) : base(ans, err)
    // {
    // }
    //
    // public BaseResponse(List<EmptyError>? err = null, params TAnswer[] ans) : base(err, ans)
    // {
    // }
    //
    // public BaseResponse(params BaseResponsePart[] errAndAns) : base(errAndAns)
    // {
    // }
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
    // public BaseResponse(List<BaseAnswerRes<TAnswer>>? ans = null, List<EmptyError>? err = null) : base(ans, err)
    // {
    // }
    //
    // public BaseResponse(List<TAnswer>? ans = null, List<EmptyError>? err = null) : base(ans, err)
    // {
    // }
    //
    // public BaseResponse(List<EmptyError>? err = null, params TAnswer[] ans) : base(err, ans)
    // {
    // }
    //
    // public BaseResponse(params BaseResponsePart[] errAndAns) : base(errAndAns)
    // {
    // }
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