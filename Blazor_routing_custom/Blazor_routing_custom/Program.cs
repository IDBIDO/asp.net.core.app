/* 1. Create WebApplicationBuilder instance
 * 2. Register the required services with the WebApplciationBuilder
 * 3. call Build() method to create the WebApplication instance
 * 4. Add middleware to create the HTTP request pipeline
 * 5. Map the endpoints in your application
 *  6. call Run() method to start the application
 */


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// serve static files in wwwroot directory
app.UseStaticFiles();

// select which endpoints handler to execute
app.UseRouting();

app.UseAuthorization();

// Map the Razor Pages routes, enabing the Razor Pages endpoints to be available at their respective URLs.
app.MapRazorPages();

// WebApplication automatically add EndPointMiddleware to the end of the pipeline.
// RoutingMiddleware is responsible for matching the request to an endpoint.
// EndPointMiddleware is responsible for executing the endpoint that matches the request.

app.Run();