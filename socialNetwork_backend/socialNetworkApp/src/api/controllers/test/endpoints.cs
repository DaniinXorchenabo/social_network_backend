using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;

namespace socialNetworkApp.api.controllers.test;


[ApiController]
[Route("api/test")]
[Produces("application/json")]
public class Test : Controller
{
    // GET
    [HttpGet("all")]
    [ValidationActionFilter]
    public PointAnswer GetAllPoints([FromQuery] Pagination pagination)
    {
        return new (new Point(1, 2), new Point(5, 1));
    }

    [HttpGet("get/{x:int}.{y:int}")]
    [ValidationActionFilter]
    public PointAnswer GetAnyPoint(int x, int y)
    {
        var d = new PointAnswer(new Point(x, y));
        return d;
    }

    [HttpPost("create")]
    [ValidationActionFilter]
    public PointAnswer CreatePoint(int x, int y)
    {
        return new PointAnswer(new Point(x, y));
    }

    [HttpPut("edit/{old_x:int}.{old_y:int}")]
    [ValidationActionFilter]
    public PointAnswer ChangeAnyPoint(int old_x, int old_y, int new_x, int new_y)
    {
        return new PointAnswer(new Point(new_x, new_y));
    }

    [HttpDelete("delete/{x:int}.{y:int}")]
    [ValidationActionFilter]
    public PointAnswer DeleteAnyPoint(int x, int y)
    {
        return new PointAnswer(new Point(x, y));
    }
}