using System.ComponentModel.DataAnnotations;

namespace ModelValidationEmail.CustomValidation;

public class PasswordValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var emailregistration = validationContext.ObjectInstance as EmailRegistration;
        
        if (emailregistration.Password.Length < 6)
        {
            return new ValidationResult("Password must be at least 6 characters long");
        }
        
        return ValidationResult.Success;
    }
}