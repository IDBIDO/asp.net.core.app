var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("pos", typeof(PositionConstraint));
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    // This middleware runs before the endpoint is selected
    await context.Response.WriteAsync("Middleware before endpoint selection\n");
    await next(context); // Call the next middleware in the pipeline
    await context.Response.WriteAsync("Middleware after endpoint selection\n");
});

// put all the middleware what depends on the endpoint routing after Routing middleware
app.UseRouting();

// At this point, the endpoint is recorded in the HttpContext (if any)
app.Use(async (context, next) =>
{
    // This middleware runs before the endpoint is selected
    await context.Response.WriteAsync("Middleware before endpoint selection\n");
    await next(context); // Call the next middleware in the pipeline
    await context.Response.WriteAsync("Middleware after endpoint selection\n");
});

app.UseEndpoints(endpoints =>   // is equivalent removing this line to use MapGet and MapPost directly
{
    endpoints.MapGet("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("List of employees");
    });
    
    endpoints.MapDelete("/employees/{id:int}", async (HttpContext context) =>   // route with parameter
    {
        await context.Response.WriteAsync($"Delete the employee with id: {context.Request.RouteValues["id"]}");
    });
    
    endpoints.MapGet("/categories/{size=medium}/{id?}", async (HttpContext context) =>    // defaulyt value for param
    {
        await context.Response.WriteAsync($"List of employees with size: {context.Request.RouteValues["size"]} and id: {context.Request.RouteValues["id"]}");
    });
    endpoints.MapGet("/employees/positions/{position:pos}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Get the employee with id: {context.Request.RouteValues["position"]}");
    });
});

app.Run();

class PositionConstraint : IRouteConstraint
{
    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (!values.ContainsKey(routeKey)) return false;
        if (values[routeKey] is null) return false;
        
        if (values[routeKey].ToString().Equals("manager", StringComparison.OrdinalIgnoreCase) ||
            values[routeKey].ToString().Equals("developer", StringComparison.OrdinalIgnoreCase))
        {
            return true; // Valid position
        }
        return false; // Invalid position
    }
}