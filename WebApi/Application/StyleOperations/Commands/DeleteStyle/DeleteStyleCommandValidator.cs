using FluentValidation;

namespace WebApi.Application.StyleOperations.Commands.DeleteStyle
{
    public class DeleteStyleCommandValidator : AbstractValidator<DeleteStyleCommand>
    {
        public DeleteStyleCommandValidator()
        {
            RuleFor(command => command.StyleId).GreaterThan(0);
        }
    }
}