using FluentValidation;

namespace StudentGradings.API.Models.Requests.Validators;

public class CreateCourseRequestValidator: AbstractValidator<CreateCourseRequest>
{
    public CreateCourseRequestValidator()
    {
        RuleFor(model => model.Name).MaximumLength(60).MinimumLength(4);
        RuleFor(model => model.Description).MaximumLength(200).MinimumLength(20);
        RuleFor(model => model.Hours).NotNull();
    }
}
