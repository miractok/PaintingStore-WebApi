using FluentValidation;

namespace WebApi.Application.MediumOperations.Commands.DeleteMedium
{
    public class DeleteMediumCommandValidator : AbstractValidator<DeleteMediumCommand>
    {
        public DeleteMediumCommandValidator()
        {
            RuleFor(command => command.MediumId).GreaterThan(0);
        }
    }
}