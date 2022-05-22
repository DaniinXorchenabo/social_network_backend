namespace socialNetworkApp.api.middlewares;

public class BaseAnswerMiddleware
{
    private readonly RequestDelegate _next;

    public BaseAnswerMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next.Invoke(context);
    }
}