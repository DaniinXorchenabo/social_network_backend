using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;

namespace socialNetworkApp.api.controllers;

public record class PointAnswer : BaseAnswerRes<Point>
{
    public PointAnswer(Point data, AnswerType type = AnswerType.pointAnswer) : base(type, data)
    {
    }
}