using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using socialNetworkApp.api.enums;

namespace socialNetworkApp.api.responses;

public class BaseAnswerRes<TData> where TData: EmptyAnswer
{
    public BaseAnswerRes(AnswerType type = default, TData data = default)
    {
        Type = type;
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public AnswerType Type { get; set; }
    public TData Data { get; set; }
}

public class EmptyAnswer : BaseResponsePart
{
    
}
