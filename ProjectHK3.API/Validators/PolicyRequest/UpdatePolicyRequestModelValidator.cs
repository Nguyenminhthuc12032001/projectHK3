using FluentValidation;
using ProjectHK3.Api.Models.PolicyRequest;
using ProjectHK3.Domain.Entities;

public class UpdatePolicyRequestModelValidator : AbstractValidator<UpdatePolicyRequestModel>
{
    public UpdatePolicyRequestModelValidator()
    {
        RuleFor(x => x.Status)
            .Must(value =>
                string.IsNullOrEmpty(value) ||
                Enum.GetNames(typeof(StatusOfPolicyRequestDetail))
                .Any(e => e.Equals(value, StringComparison.OrdinalIgnoreCase))
            )
            .WithMessage("Invalid status value.");

        RuleFor(x => x.Remarks)
            .MaximumLength(500)
            .WithMessage("Remarks cannot exceed 500 characters.");
    }
}
