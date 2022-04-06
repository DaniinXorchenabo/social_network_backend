using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.test;

[AddAnswerType(AnswerType.PointAnswer)]

public record class Point(int X = 0, int Y = 0) : EmptyAnswer;