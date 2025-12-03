using FluentValidation;
using ProjectHK3.Api.Models.Auth;

public class NewAdminModelValidator : AbstractValidator<NewAdminModel>
{
    public NewAdminModelValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.Role)
            .Must(r => r == "Admin" || r == "Manager" || r == "Finance")
            .WithMessage("Role must be Admin, Manager or Finance.");
    }
}
