using FluentValidation;

namespace StudentGradings.API.Models.Requests.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(model => model.Name).MaximumLength(60).MinimumLength(2);
        RuleFor(model => model.LastName).MaximumLength(60).MinimumLength(2);
        RuleFor(model => model.Phone).MaximumLength(12).MinimumLength(12);
        RuleFor(model => model.Email).EmailAddress();
        RuleFor(model => model.Password).NotEmpty().MinimumLength(8);
    }
}
