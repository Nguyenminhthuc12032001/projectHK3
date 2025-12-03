using FluentValidation;
using ProjectHK3.Api.Models.Auth;

public class ChangePasswordAdminModelValidator : AbstractValidator<ChangePasswordModel.Admin>
{
    public ChangePasswordAdminModelValidator()
    {
        RuleFor(x => x.AdminId).GreaterThan(0);

        RuleFor(x => x.CurrentPassword).NotEmpty();
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(6);
    }
}
