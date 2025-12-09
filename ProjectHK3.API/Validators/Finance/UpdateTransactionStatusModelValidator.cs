using FluentValidation;
using ProjectHK3.Api.Models.Finance;
using ProjectHK3.Domain.Entities;

public class UpdateTransactionStatusModelValidator : AbstractValidator<UpdateTransactionStatusModel>
{
    public UpdateTransactionStatusModelValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty()
            .Must(s => Enum.TryParse(typeof(StatusOfTransactionLedger), s, true, out _))
            .WithMessage("Status must be Pending, Completed, Failed, or Reserved");
    }
}
