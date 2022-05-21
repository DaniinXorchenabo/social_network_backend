using socialNetworkApp.api.attributes;

namespace socialNetworkApp.api.responses.error;

public class ValidateError<TEnum>: BaseErrorRes where TEnum: Enum
{
    [DictEnumSwagger]
    public Dictionary<string, ValidateOneField> Errors { get; set; }
    public TEnum testEnum { get; set; }
    
}

// public  class ValidateError//: BaseErrorRes
// {
//     // public Dictionary<string, ValidateOneField> Errors { get; set; }
//     
// }


public  class ValidateOneField
{
    public string FieldName { get; set; }
    public List<OneFieldErrorValidate> Errors { get; set; }
    public string ValidationState { get; set; }
}

   public class OneFieldErrorValidate
{
    public string? Exception { get; set; }
    public string? ErrorMessage { get; set; }
}