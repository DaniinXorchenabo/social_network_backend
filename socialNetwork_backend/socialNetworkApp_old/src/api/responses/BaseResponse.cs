namespace socialNetworkApp.api.responses;

public record BaseResponse<Error, Answer>(
    List<Error>? errors = null,
    List<Answer>? answers = null
    );