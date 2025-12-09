using FluentValidation;
using ProjectHK3.Api.Models.PolicyRequest;
using ProjectHK3.Domain.Entities;

public class PolicyRequestDocumentModelValidator : AbstractValidator<PolicyRequestDocumentModel>
{
    public PolicyRequestDocumentModelValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("File name is required.")
            .MaximumLength(255);

        RuleFor(x => x.FileURL)
            .NotEmpty().WithMessage("File URL is required.")
            .MaximumLength(500);

        RuleFor(x => x.DocumentType)
            .Must(value =>
                Enum.GetNames(typeof(DocumentTypeOfPolicyRequestDocument))
                .Any(e => e.Equals(value, StringComparison.OrdinalIgnoreCase))
            )
            .WithMessage("Invalid document type.");

        RuleFor(x => x.UploadedBy)
            .GreaterThan(0)
            .WithMessage("UploadedBy must be valid user.");

        RuleFor(x => x.Status)
            .Must(value =>
                Enum.GetNames(typeof(StatusOfPolicyRequestDocument))
                .Any(e => e.Equals(value, StringComparison.OrdinalIgnoreCase))
            )
            .WithMessage("Invalid document status.");
    }
}
