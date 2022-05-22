using socialNetworkApp.api.attributes;

namespace socialNetworkApp.api.responses.error;

public class ValidateError : BaseErrorRes
{
    public virtual Dictionary<string, ValidateOneField> Errors { get; set; }
}

public class ValidateError<TEnum> : ValidateError where TEnum : Enum
{
    [DictEnumSwagger] public override Dictionary<string, ValidateOneField> Errors { get; set; }

    public TEnum testEnum { get; set; }
}

public class ValidateOneField
{
    public string FieldName { get; set; }
    public List<OneFieldErrorValidate> Errors { get; set; }
    public Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState ValidationState { get; set; }
}

public class OneFieldErrorValidate
{
    public string? Exception { get; set; }
    public string? ErrorMessage { get; set; }
}