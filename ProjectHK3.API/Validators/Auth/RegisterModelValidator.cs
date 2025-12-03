using FluentValidation;
using ProjectHK3.Api.Models.Auth;

public class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    public RegisterModelValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.");

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress();

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{9,11}$").WithMessage("Phone must be 9-11 digits.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.Department)
            .NotEmpty();

        RuleFor(x => x.HireDate)
            .NotEmpty().WithMessage("Hire date is required.");

        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("Invalid company.");
    }
}
