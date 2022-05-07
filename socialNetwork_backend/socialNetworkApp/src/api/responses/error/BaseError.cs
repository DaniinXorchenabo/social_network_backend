using socialNetworkApp.api.enums;

namespace socialNetworkApp.api.responses;

public record class BaseErrorRes : EmptyError
{
    public ErrorType Type;
    public string Name;
    public string? Summary;
    public string? Description = null;
}

public record class EmptyError : BaseResponsePart
{
    
}