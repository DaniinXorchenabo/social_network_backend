using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.test;

[AddAnswerType(AnswerType.pointAnswer)]

public record class Point(int x = 0, int y = 0) : EmptyAnswer;