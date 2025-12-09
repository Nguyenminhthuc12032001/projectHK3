using FluentValidation;
using ProjectHK3.Api.Models.Insurer;

public class UpdateInsurerModelValidator : AbstractValidator<UpdateInsurerModel>
{
    public UpdateInsurerModelValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Invalid Insurer Id.");

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Address)
            .NotEmpty();

        RuleFor(x => x.ContactEmail)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(150);

        RuleFor(x => x.ContactPhone)
            .NotEmpty()
            .Matches(@"^\+?\d{8,20}$");

        RuleFor(x => x.Website)
            .MaximumLength(150)
            .When(x => !string.IsNullOrEmpty(x.Website))
            .Matches(@"^https?:\/\/.*$")
            .WithMessage("Website must be a valid URL.")
            .When(x => !string.IsNullOrWhiteSpace(x.Website));
    }
}
