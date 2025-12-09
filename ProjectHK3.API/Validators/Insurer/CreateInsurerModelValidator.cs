using FluentValidation;
using ProjectHK3.Api.Models.Insurer;

public class CreateInsurerModelValidator : AbstractValidator<CreateInsurerModel>
{
    public CreateInsurerModelValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company name is required.")
            .MaximumLength(200).WithMessage("Company name must be <= 200 characters.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");

        RuleFor(x => x.ContactEmail)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(150);

        RuleFor(x => x.ContactPhone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{8,20}$")
            .WithMessage("Phone must be between 8–20 digits.");

        RuleFor(x => x.Website)
            .NotEmpty()
            .MaximumLength(150)
            .WithMessage("Website must be <=150 characters.")
            .Matches(@"^https?:\/\/.*$")
            .WithMessage("Website must be a valid URL.");
    }
}
