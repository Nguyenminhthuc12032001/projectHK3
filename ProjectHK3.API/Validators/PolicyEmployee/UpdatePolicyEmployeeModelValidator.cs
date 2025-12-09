using FluentValidation;
using ProjectHK3.Api.Models.PolicyEmployee;

public class UpdatePolicyEmployeeModelValidator : AbstractValidator<UpdatePolicyEmployeeModel>
{
    private readonly string[] allowedStatuses = { "Active", "Expired", "Cancelled" };

    public UpdatePolicyEmployeeModelValidator()
    {
        RuleFor(x => x.Status)
            .Must(s => s == null || allowedStatuses.Contains(s))
            .WithMessage("Invalid Status value. Allowed: Active, Expired, Cancelled");

        RuleFor(x => x.EndDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .When(x => x.EndDate.HasValue)
            .WithMessage("EndDate must be later than today.");
    }
}
