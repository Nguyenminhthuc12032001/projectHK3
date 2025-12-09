using FluentValidation;
using ProjectHK3.Api.Models.PolicyApproval;

public class PolicyApprovalRequestModelValidator : AbstractValidator<PolicyApprovalRequestModel>
{
    public PolicyApprovalRequestModelValidator()
    {
        RuleFor(x => x.PolicyRequestId)
            .GreaterThan(0)
            .WithMessage("Invalid policy request.");

        RuleFor(x => x.AdminId)
            .GreaterThan(0)
            .WithMessage("Invalid AdminId.");

        RuleFor(x => x.Remarks)
            .MaximumLength(500)
            .WithMessage("Remarks cannot exceed 500 characters.");
    }
}
