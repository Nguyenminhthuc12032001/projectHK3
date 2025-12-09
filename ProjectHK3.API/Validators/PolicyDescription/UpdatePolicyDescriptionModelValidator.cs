using FluentValidation;
using ProjectHK3.Api.Models.PolicyDescription;

public class UpdatePolicyDescriptionModelValidator : AbstractValidator<UpdatePolicyDescriptionModel>
{
    public UpdatePolicyDescriptionModelValidator()
    {
        RuleFor(x => x.PolicySummary).MaximumLength(500)
            .When(x => x.PolicySummary is not null);

        RuleFor(x => x.Benefits).NotEmpty()
            .When(x => x.Benefits is not null);

        RuleFor(x => x.Exclusions).NotEmpty()
            .When(x => x.Exclusions is not null);

        RuleFor(x => x.TermsConditions).NotEmpty()
            .When(x => x.TermsConditions is not null);
    }
}
