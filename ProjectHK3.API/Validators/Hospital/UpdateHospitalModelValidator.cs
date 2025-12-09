using FluentValidation;
using ProjectHK3.Api.Models.Hospital;

public class UpdateHospitalModelValidator : AbstractValidator<UpdateHospitalModel>
{
    public UpdateHospitalModelValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.HospitalName)
            .NotEmpty().WithMessage("Hospital name is required.");

        RuleFor(x => x.ContactPhone)
            .Matches(@"^\+?[0-9]{8,15}$")
            .When(x => !string.IsNullOrEmpty(x.ContactPhone));

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));
    }
}
