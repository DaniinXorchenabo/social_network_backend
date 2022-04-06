using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using socialNetworkApp.api.enums;

namespace socialNetworkApp.api.responses;

public record BaseAnswerRes<TData>(AnswerType Type, TData Data);

public record EmptyAnswer : BaseResponsePart
{
    
}
