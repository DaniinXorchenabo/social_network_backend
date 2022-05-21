using socialNetworkApp.api.enums;

namespace socialNetworkApp.api.responses.utils;

public class AddAnswerTypeAttribute: Attribute
{
    public AnswerType Type { get; set; }

    public AddAnswerTypeAttribute()
    {
    }

    public AddAnswerTypeAttribute(AnswerType type)
    {
        this.Type = type;
    }
}