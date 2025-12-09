using FluentValidation;
using ProjectHK3.Api.Models.PolicyRequest;
using ProjectHK3.Domain.Entities;

public class CreatePolicyRequestModelValidator : AbstractValidator<CreatePolicyRequestModel>
{
    public CreatePolicyRequestModelValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0)
            .WithMessage("EmployeeId must be greater than 0.");

        RuleFor(x => x.PolicyId)
            .GreaterThan(0)
            .WithMessage("PolicyId must be greater than 0.");

        RuleFor(x => x.RequestType)
            .NotEmpty().WithMessage("Request type is required.")
            .Must(value =>
                Enum.GetNames(typeof(RequestTypeOfPolicyRequestDetail))
                .Any(e => e.Equals(value, StringComparison.OrdinalIgnoreCase))
            )
            .WithMessage("Invalid request type. Allowed: Enrollment, Claim, Cancel.");

        RuleFor(x => x.Remarks)
            .MaximumLength(500)
            .WithMessage("Remarks cannot exceed 500 characters.");
    }
}
