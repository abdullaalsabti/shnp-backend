using FluentValidation;
using PhoneNumbers;
using WebApplication1.Dto;

namespace WebApplication1.Validation;

public class UserRegistrationValidator : AbstractValidator<UserForRegistration>
{
    public UserRegistrationValidator()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage("Restaurant name is required")
            .MinimumLength(3).MaximumLength(100).WithMessage("Restaurant name must be between 3 and 100 characters")
            .Matches(@"^[A-Za-z\s]+$").WithMessage("English name must contain only English letters and spaces");

        RuleFor(x => x.NameAr)
            .NotEmpty().WithMessage("Restaurant name is required")
            .MinimumLength(3).MaximumLength(100).WithMessage("Restaurant name must be between 3 and 100 characters")
            .Matches(@"^[\u0600-\u06FF\s]+$").WithMessage("Arabic name must contain only Arabic letters and spaces");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Must be a valid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must contain at least 6 characters");
        // .Equal(x => x.ConfirmPassword).WithMessage("Passwords do not match");

        // RuleFor(x => x.ConfirmPassword)
        //     .NotEmpty().WithMessage("Confirm password is required")
        //     .MinimumLength(6).WithMessage("Password must contain at least 6 characters")
        //     .Equal(x => x.Password).WithMessage("Passwords do not match");

        RuleFor(x => x.BankAccountIban)
            .NotEmpty().WithMessage("IBAN is required")
            .Matches(@"^[A-Z]{2}[0-9]{2}[A-Z0-9]{11,30}$").WithMessage("Invalid IBAN format");

        RuleFor(x => x.FoodCategories)
            .NotEmpty().WithMessage("Restaurant type is required");

        RuleFor(x => x.OperationRepresentativeEmailAddress)
            .NotEmpty().WithMessage("Operation representative email is required")
            .EmailAddress().WithMessage("Must be a valid email address");

        RuleFor(x => x.OperationRepresentativeFullNameAr)
            .NotEmpty().WithMessage("Operation representative name is required")
            .MinimumLength(3).MaximumLength(100)
            .WithMessage("Operation representative name must be between 3 and 100 characters")
            .Matches(@"^[\u0600-\u06FF\s]+$").WithMessage("Must be in Arabic format");

        RuleFor(x => x.OperationRepresentativeFullNameEn)
            .NotEmpty().WithMessage("Operation representative name is required")
            .MinimumLength(3).MaximumLength(100)
            .WithMessage("Operation representative name must be between 3 and 100 characters")
            .Matches(@"^[A-Za-z\s]+$").WithMessage("Must be in English format");

        RuleFor(x => x.ManagementPhoneNumber)
            .NotEmpty().WithMessage("Management phone number is required")
            .Must(IsValidPhoneNumber).WithMessage("Invalid phone number");

        RuleFor(x => x.OperationRepresentativePhoneNumber)
            .NotEmpty().WithMessage("Operation representative phone number is required")
            .Must(IsValidPhoneNumber).WithMessage("Invalid phone number");
    }

    private bool IsValidPhoneNumber(string number)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            var parsedNumber = phoneNumberUtil.Parse(number, null);
            return phoneNumberUtil.IsValidNumber(parsedNumber);
        }
        catch
        {
            return false;
        }
    }
}
