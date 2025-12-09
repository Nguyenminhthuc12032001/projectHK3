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
            .Matches(@"^\+[1-9]\d{1,14}$")
            .WithMessage("Phone must follow international format (E.164). Example: +84912345678");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.Department)
            .NotEmpty();

        RuleFor(x => x.HireDate)
            .NotEmpty()
            .Matches(@"^\d{4}-\d{2}-\d{2}$")
            .WithMessage("Hire date must be in format yyyy-MM-dd.")
            .Must(date => DateOnly.TryParse(date, out _))
            .WithMessage("Hire date is invalid.");

        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("Invalid company.");
    }
}
