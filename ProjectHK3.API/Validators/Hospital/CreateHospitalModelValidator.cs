using FluentValidation;
using ProjectHK3.Api.Models.Hospital;

public class CreateHospitalModelValidator : AbstractValidator<CreateHospitalModel>
{
    public CreateHospitalModelValidator()
    {
        RuleFor(x => x.HospitalName)
            .NotEmpty().WithMessage("Hospital name is required.")
            .MaximumLength(200);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(100);

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.ContactPhone)
            .NotEmpty().WithMessage("Contact phone is required.")
            .Matches(@"^\+?[0-9]{8,15}$")
            .WithMessage("Invalid phone number format.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress();

        RuleFor(x => x.RegisteredOn)
            .NotEmpty()
            .Must(date => DateOnly.TryParse(date, out _))
            .WithMessage("RegisteredOn must be a valid date yyyy-MM-dd.");
    }
}
