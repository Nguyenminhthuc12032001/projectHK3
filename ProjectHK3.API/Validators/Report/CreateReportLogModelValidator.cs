using FluentValidation;
using ProjectHK3.Api.Models.Report;

public class CreateReportLogModelValidator : AbstractValidator<CreateReportLogModel>
{
    public CreateReportLogModelValidator()
    {
        RuleFor(x => x.ReportType)
            .NotEmpty().WithMessage("ReportType is required.")
            .MaximumLength(100);

        RuleFor(x => x.GeneratedBy)
            .GreaterThan(0).WithMessage("GeneratedBy (AdminId) is invalid.");
    }
}
