using socialNetworkApp.api.enums;

namespace socialNetworkApp.api.responses;

public record class BaseErrorRes(ErrorType type,
    string name, string? summary, string? description = null);