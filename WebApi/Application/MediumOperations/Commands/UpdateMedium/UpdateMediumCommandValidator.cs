using FluentValidation;

namespace WebApi.Application.MediumOperations.Commands.UpdateMedium
{
    public class UpdateMediumCommandValidator : AbstractValidator<UpdateMediumCommand>
    {
        public UpdateMediumCommandValidator()
        {
            RuleFor(command => command.MediumId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(1).NotEmpty().NotEqual("string");
        }
    }
}