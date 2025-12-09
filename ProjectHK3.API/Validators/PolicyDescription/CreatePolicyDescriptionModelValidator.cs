using FluentValidation;
using ProjectHK3.Api.Models.PolicyDescription;

public class CreatePolicyDescriptionModelValidator : AbstractValidator<CreatePolicyDescriptionModel>
{
    public CreatePolicyDescriptionModelValidator()
    {
        RuleFor(x => x.PolicySummary).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Benefits).NotEmpty();
        RuleFor(x => x.Exclusions).NotEmpty();
        RuleFor(x => x.TermsConditions).NotEmpty();
    }
}
