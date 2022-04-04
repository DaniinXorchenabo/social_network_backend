namespace socialNetworkApp.api.middlewares;

public class BaseAnswerMiddleware
{
    private readonly RequestDelegate next;
    
    public BaseAnswerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await next.Invoke(context);
        
    }
}