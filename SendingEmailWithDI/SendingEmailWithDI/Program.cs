// Initilize WebApplicationBuilder

using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services using extension method
builder.Services.AddEmailSender();

// Add explicit configurations
// builder.Services.AddSingleton<NetworkClient>();
// builder.Services.AddScoped<MessageFactory>();
// builder.Services.AddSingleton(provider =>
//     new EmailServerSettings
//     (
//         Host: "smtp.server.com",
//         Port: 25
//     ));

// build the app
var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapGet("/register/{username}", RegisterUser);
app.MapGet("/", () => "Navigate to register/{username} to test the Mock email sending");

app.Run();

string RegisterUser(string username, IEmailSender emailSender)
{
    emailSender.SendEmail(username);
    return $"Email sent to {username}!";
}

public record Email(string Address, string Message);

public record EmailServerSettings(string Host, int Port);

public interface IEmailSender
{
    void SendEmail(string username);
}

public class EmailSender : IEmailSender
{
    private readonly NetworkClient _client;
    private readonly MessageFactory _factory;
    
    public EmailSender(MessageFactory factory, NetworkClient client)
    {
        _factory = factory;
        _client = client;
    }

    public void SendEmail(string username)
    {
        var email = _factory.Create(username);
        _client.SendEmail(email);
        Console.WriteLine($"Email sent to {username}!");
    }
}

public class NetworkClient
{
    private readonly EmailServerSettings _settings;

    public NetworkClient(EmailServerSettings settings)
    {
        _settings = settings;
    }

    public void SendEmail(Email email)
    {
        Console.WriteLine($"Connecting to server {_settings.Host}:{_settings.Port}");
        Console.WriteLine($"Email sent to {email.Address}: {email.Message}");
    }
}

public class MessageFactory
{
    public Email Create(string emailAddress)
        => new Email(emailAddress, "Thanks for signing up!");
}

public static class EmailSenderServiceCollectionExtensions
{
    public static IServiceCollection AddEmailSender(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddSingleton<NetworkClient>();
        services.AddSingleton<MessageFactory>();
        services.AddSingleton(
            new EmailServerSettings
            (
                "smtp.example.com",
                25
            ));
        return services;
    }
}

