using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;

namespace socialNetworkApp.api.controllers;


[ApiController]
[Route("api/test")]
public class Test : Controller
{
    // GET
    [HttpGet("all")]
    public Point[] GetAllPoints()
    {
        return new[] {new Point(1, 2), new Point(5, 1)};
    }

    [HttpGet("get/{x:int}.{y:int}")]
    public PointAnswer GetAnyPoint(int x, int y)
    {
        var d = new PointAnswer(new Point(x, y)) { };
        return d;
    }

    [HttpPost("create")]
    public Point CreatePoint(int x, int y)
    {
        return new Point(x, y);
    }

    [HttpPut("edit/{old_x:int}.{old_y:int}")]
    public Point ChangeAnyPoint(int old_x, int old_y, int new_x, int new_y)
    {
        return new Point(new_x, new_y);
    }

    [HttpDelete("delete/{x:int}.{y:int}")]
    public Point DeleteAnyPoint(int x, int y)
    {
        return new Point(x, y);
    }
}