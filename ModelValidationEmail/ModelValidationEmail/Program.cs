using System.ComponentModel.DataAnnotations;
using ModelValidationEmail.CustomValidation;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/registrations", () =>
{
    var registrations = EmailRegistrationRepository.GetRegistrations();
    return Results.Ok(registrations);
});

/*
app.MapGet("/resgister", (Registration reg) =>
{
    return $"User {reg.Email} registered successfully.";
}).WithParameterValidation();
*/

app.MapPost("/register", (EmailRegistration registration) =>
{
    EmailRegistrationRepository.AddRegistration(registration);

    // Here you would typically save the registration to a database
    return Results.Ok($"User {registration.Email} registered successfully.");
}).WithParameterValidation();

app.Run();


public static class EmailRegistrationRepository
{
    private static List<EmailRegistration> registrations = new List<EmailRegistration>();

    public static List<EmailRegistration> GetRegistrations() => registrations;

    public static void AddRegistration(EmailRegistration registration)
    {
        registrations.Add(registration);
    }
}

public class EmailRegistration
{
    // [EmailAdreess (ErrorMessage = "Email is not valid.")]
    [Required]
    [EmailValidation]
    public string Email { get; set; }
    
    // [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    [Required]
    [PasswordValidation]
    public string Password { get; set; }
    
    // [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
    [Required]
    [ConfirmPasswordValidation]
    public string ConfirmPassword { get; set; }
    
    public static ValueTask<EmailRegistration> BindAsync(HttpContext context)
    {
        var email = context.Request.Query["Email"];
        var password = context.Request.Query["Password"];
        var confirmPassword = context.Request.Query["ConfirmPassword"];

        return new ValueTask<EmailRegistration>(new EmailRegistration() 
        {
            Email = email,
            Password = password,
            ConfirmPassword = confirmPassword
        });
    }
    
}

