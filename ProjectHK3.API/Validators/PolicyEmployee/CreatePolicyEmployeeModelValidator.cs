using FluentValidation;
using ProjectHK3.Api.Models.PolicyEmployee;

public class CreatePolicyEmployeeModelValidator : AbstractValidator<CreatePolicyEmployeeModel>
{
    public CreatePolicyEmployeeModelValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0)
            .WithMessage("Invalid EmployeeId.");

        RuleFor(x => x.PolicyId)
            .GreaterThan(0)
            .WithMessage("Invalid PolicyId.");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("StartDate is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("EndDate is required.")
            .GreaterThan(x => x.StartDate)
            .WithMessage("EndDate must be after StartDate.");
    }
}
