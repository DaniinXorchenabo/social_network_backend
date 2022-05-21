using socialNetworkApp.api.enums;

namespace socialNetworkApp.api.responses;

public  class BaseErrorRes : EmptyError
{
    public ErrorType Type { get; set; }
    public string Name{ get; set; }
    public string? Summary{ get; set; }
    public string? Description{ get; set; } = null;
}

public  class EmptyError : BaseResponsePart
{
    
}