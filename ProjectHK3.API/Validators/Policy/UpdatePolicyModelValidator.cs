using FluentValidation;
using ProjectHK3.Api.Models.Policy;

public class UpdatePolicyModelValidator : AbstractValidator<UpdatePolicyModel>
{
    public UpdatePolicyModelValidator()
    {
        RuleFor(x => x.PolicyName)
            .MaximumLength(150)
            .When(x => !string.IsNullOrWhiteSpace(x.PolicyName));

        RuleFor(x => x.CoverageAmount)
            .GreaterThan(0)
            .When(x => x.CoverageAmount.HasValue);

        RuleFor(x => x.PremiumAmount)
            .GreaterThan(0)
            .When(x => x.PremiumAmount.HasValue);

        RuleFor(x => x.EffectiveTo)
            .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .When(x => x.EffectiveTo.HasValue)
            .WithMessage("Effective To must be a future date.");

        RuleFor(x => x.Status)
            .Must(s => new[] { "Draft", "Active", "Expired" }.Contains(s!, StringComparer.OrdinalIgnoreCase))
            .When(x => !string.IsNullOrWhiteSpace(x.Status))
            .WithMessage("Status must be Draft, Active, or Expired.");
    }
}
