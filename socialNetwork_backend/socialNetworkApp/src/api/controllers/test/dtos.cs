using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.test;

[AddAnswerType(AnswerType.PointAnswer)]

public class Point : AbstractDto
{
    public int X{ get; set; } = 0;
    public int Y{ get; set; } = 0;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public Point(object obj) : base(obj){}
    
    public Point(){}
}