using FluentValidation;
using ProjectHK3.Api.Models.Auth;

public class ChangePasswordEmployeeModelValidator : AbstractValidator<ChangePasswordModel.Employee>
{
    public ChangePasswordEmployeeModelValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0);

        RuleFor(x => x.CurrentPassword).NotEmpty();
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(6);
    }
}
