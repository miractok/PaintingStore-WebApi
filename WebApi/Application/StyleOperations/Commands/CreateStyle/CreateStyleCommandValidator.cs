using FluentValidation;

namespace WebApi.Application.StyleOperations.Commands.CreateStyle
{
    public class CreateStyleCommandValidator : AbstractValidator<CreateStyleCommand>
    {
        public CreateStyleCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(1).NotEqual("string");
        }
    }
}