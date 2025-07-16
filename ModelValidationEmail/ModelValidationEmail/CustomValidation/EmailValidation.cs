using System.ComponentModel.DataAnnotations;

namespace ModelValidationEmail.CustomValidation;

public class EmailValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var emailRegistration = validationContext.ObjectInstance as EmailRegistration;
        
        if (!emailRegistration.Email.Contains("@") ||
            !emailRegistration.Email.Contains(".com"))
        {
            return new ValidationResult("Email is not valid.");
        }
        
        return ValidationResult.Success;
        
    }
}