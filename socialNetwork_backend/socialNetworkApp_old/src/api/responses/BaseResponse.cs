namespace socialNetworkApp.api.responses;

public record BaseResponse<Answer, Error>(
    List<Answer>? answers = null,
    List<Error>? errors = null

    );