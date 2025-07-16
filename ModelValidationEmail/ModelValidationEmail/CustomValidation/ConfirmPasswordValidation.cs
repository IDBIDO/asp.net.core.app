using System.ComponentModel.DataAnnotations;

namespace ModelValidationEmail.CustomValidation;

public class ConfirmPasswordValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var emailRegistration = validationContext.ObjectInstance as EmailRegistration;

        if (emailRegistration.Password != emailRegistration.ConfirmPassword)
        {
            return new ValidationResult("Password and Confirm Password do not match.");
        }

        return ValidationResult.Success;
    }
}