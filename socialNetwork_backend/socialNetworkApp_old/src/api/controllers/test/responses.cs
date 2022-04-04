﻿using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;

namespace socialNetworkApp.api.controllers;

public record class PointAnswer : BaseResponse<BaseAnswerRes<Point>, EmptyError?>
{
    public PointAnswer(Point data, AnswerType type = AnswerType.pointAnswer) :
        base(new List<BaseAnswerRes<Point>>{new (type, data)}, null)
    {
    }
}

public record class ManyPointsAnswer : BaseResponse<BaseAnswerRes<Point>, EmptyError?>
{
    public ManyPointsAnswer(List<Point> data, AnswerType type = AnswerType.pointAnswer) :
        base(data.Select(x => new BaseAnswerRes<Point>(type, x)).ToList())
    {
    }
}

