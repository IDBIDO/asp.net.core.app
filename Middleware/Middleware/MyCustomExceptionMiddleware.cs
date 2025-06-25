namespace Middleware;

public class MyCustomExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            // here is the first middleware logic, so we can write some response headers or do some logging
            context.Response.ContentType = "text/html";
            await next(context);
        }
        catch (Exception ex)
        {
            await context.Response.WriteAsync("<h1>An error occurred</h1>");
            await context.Response.WriteAsync($"<p>{ex.Message}</p>");
        }
    }
}