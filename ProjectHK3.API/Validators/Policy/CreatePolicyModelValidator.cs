using FluentValidation;
using ProjectHK3.Api.Models.Policy;

public class CreatePolicyModelValidator : AbstractValidator<CreatePolicyModel>
{
    public CreatePolicyModelValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

        RuleFor(x => x.PolicyName)
            .NotEmpty().WithMessage("Policy name is required.")
            .MaximumLength(150).WithMessage("Policy name cannot exceed 150 characters.");

        RuleFor(x => x.CoverageAmount)
            .GreaterThan(0).WithMessage("Coverage amount must be greater than 0.");

        RuleFor(x => x.PremiumAmount)
            .GreaterThan(0).WithMessage("Premium amount must be greater than 0.");

        RuleFor(x => x.EffectiveFrom)
            .NotEmpty().WithMessage("Effective From date is required.");

        RuleFor(x => x.EffectiveTo)
            .GreaterThan(x => x.EffectiveFrom)
            .WithMessage("Effective To must be later than Effective From.");

        RuleFor(x => x.Status)
            .Must(s => new[] { "Draft", "Active", "Expired" }.Contains(s, StringComparer.OrdinalIgnoreCase))
            .WithMessage("Status must be either Draft, Active, or Expired.");
    }
}
