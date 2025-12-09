using FluentValidation;
using ProjectHK3.Api.Models.Notification;

public class SendNotificationModelValidator : AbstractValidator<SendNotificationModel>
{
    public SendNotificationModelValidator()
    {
        RuleFor(x => x.Recipient)
            .NotEmpty().WithMessage("Recipient is required.")
            .MaximumLength(150);

        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(x => x == "Email" || x == "SMS")
            .WithMessage("Type must be Email or SMS.");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required.")
            .MaximumLength(255);

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required.");
    }
}
