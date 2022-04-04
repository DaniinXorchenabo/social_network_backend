using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using socialNetworkApp.api.enums;

namespace socialNetworkApp.api.responses;

public record BaseAnswerRes<Data>
{
    [Required]
    public AnswerType type { get; set; }
    [Required]
    // [EnumDataType(typeof(Priority))]
    [JsonConverter(typeof(StringEnumConverter))]
    public Data data { get; set; }
    
    public BaseAnswerRes(AnswerType type, Data data){
        this.type = type;
        this.data = data;
    }
}
