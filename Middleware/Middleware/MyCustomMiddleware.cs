namespace Middleware;

public class MyCustomMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("MyCustomMiddleware is running...\n");
    
        // short-circuit the pipeline to skip the next middleware
        await next(context);
        await context.Response.WriteAsync("MyCustomMiddleware has finished processing.\n");
    }
}