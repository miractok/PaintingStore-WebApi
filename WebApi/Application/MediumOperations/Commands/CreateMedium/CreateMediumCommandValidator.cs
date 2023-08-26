using FluentValidation;

namespace WebApi.Application.MediumOperations.Commands.CreateMedium
{
    public class CreateMediumCommandValidator : AbstractValidator<CreateMediumCommand>
    {
        public CreateMediumCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(1).NotEqual("string");
        }
    }
}