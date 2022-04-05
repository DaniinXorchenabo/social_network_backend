using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using socialNetworkApp.api.enums;

namespace socialNetworkApp.api.responses;

public record BaseAnswerRes<TData>(AnswerType type, TData data);

public record EmptyAnswer : BaseResponsePart
{
    
}
