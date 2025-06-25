using Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyCustomMiddleware>();
builder.Services.AddTransient<MyCustomExceptionMiddleware>();

var app = builder.Build();


app.UseMiddleware<MyCustomExceptionMiddleware>();

// Middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware1 is running...\n");
    await next(context);
    await context.Response.WriteAsync("Middleware1 has finished processing.\n");
});

// To create a rejoinable middleware pipeline, we can use the `UseWhen` method.
app.UseWhen((context) => context.Request.Path.StartsWithSegments("/rejoin"),
    (appBuilder) => 
{
    // Middleware R1
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("MiddlewareR1 is running...\n");
    
        // short-circuit the pipeline to skip the next middleware
        await next(context);
        await context.Response.WriteAsync("MiddlewareR1 has finished processing.\n");
    });
    
    // Middleware R2
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("MiddlewareR2 is running...\n");
    
        // short-circuit the pipeline to skip the next middleware
        await next(context);
        await context.Response.WriteAsync("MiddlewareR2 has finished processing.\n");
    });
});

// Like app.Map but it allows for a condition to be specified.
app.MapWhen((context) => context.Request.Query.ContainsKey("id"),
    (appBuilder) => 
{
    // Middleware 7
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware7 is running...\n");
    
        // short-circuit the pipeline to skip the next middleware
        await next(context);
        await context.Response.WriteAsync("Middleware7 has finished processing.\n");
    });
    
    // Middleware 8
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware8 is running...\n");
    
        // short-circuit the pipeline to skip the next middleware
        await next(context);
        await context.Response.WriteAsync("Middleware8 has finished processing.\n");
    });
});

// To create a path-specific middleware pipeline, we can use the `Map` method.
app.Map("/employees", (appBuilder) => 
{
    // Middleware 5
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware5 is running...\n");
    
        // short-circuit the pipeline to skip the next middleware
        await next(context);
        await context.Response.WriteAsync("Middleware5 has finished processing.\n");
    });
    
    // Middleware 6
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware6 is running...\n");
    
        // short-circuit the pipeline to skip the next middleware
        await next(context);
        await context.Response.WriteAsync("Middleware6 has finished processing.\n");
    });
});

// Middleware 2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    throw new ApplicationException("Exception in Middleware1");
    await context.Response.WriteAsync("Middleware2 is running...\n");
    
    // short-circuit the pipeline to skip the next middleware
    await next(context);
    await context.Response.WriteAsync("Middleware2 has finished processing.\n");
});

app.UseMiddleware<MyCustomMiddleware>();

// Middleware 3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware3 is running...\n");
    await next(context);
    await context.Response.WriteAsync("Middleware3 has finished processing.\n");
});

app.Run();