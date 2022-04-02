using Microsoft.AspNetCore.Mvc;

namespace socialNetworkApp.api.controllers;

[Route("test")]
public class Test : Controller
{
    // GET
    [HttpGet("test")]
    public string Index()
    {
        return "hello";
    }
}