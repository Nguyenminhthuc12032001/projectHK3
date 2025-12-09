using FluentValidation;
using ProjectHK3.Api.Models.Finance;
using ProjectHK3.Domain.Entities;

public class CreateTransactionModelValidator : AbstractValidator<CreateTransactionModel>
{
    public CreateTransactionModelValidator()
    {
        RuleFor(x => x.ApprovalId)
            .GreaterThan(0).WithMessage("Invalid approval id.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.Method)
            .NotEmpty()
            .Must(m => Enum.TryParse(typeof(MethodOfTransactionLedger), m, true, out _))
            .WithMessage("Method must be BankTransfer, Ewallet, Cash, or Refund");
    }
}
