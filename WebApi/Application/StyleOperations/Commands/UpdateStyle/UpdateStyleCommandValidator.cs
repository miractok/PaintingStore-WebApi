using FluentValidation;

namespace WebApi.Application.StyleOperations.Commands.UpdateStyle
{
    public class UpdateStyleCommandValidator : AbstractValidator<UpdateStyleCommand>
    {
        public UpdateStyleCommandValidator()
        {
            RuleFor(command => command.StyleId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(1).NotEmpty().NotEqual("string");
        }
    }
}