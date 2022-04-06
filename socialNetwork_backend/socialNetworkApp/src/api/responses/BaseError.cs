using socialNetworkApp.api.enums;

namespace socialNetworkApp.api.responses;

public record class BaseErrorRes(ErrorType Type,
    string Name, string? Summary, string? Description = null) : EmptyError;

public record EmptyError : BaseResponsePart
{
    
}