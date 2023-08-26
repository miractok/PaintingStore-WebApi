using FluentValidation;

namespace WebApi.Application.PaintingOperations.Commands.DeletePainting
{
    public class DeletePaintingCommandValidator : AbstractValidator<DeletePaintingCommand>
    {
        public DeletePaintingCommandValidator()
        {
            RuleFor(command => command.PaintingId).GreaterThan(0).NotEmpty().NotNull();
        }
    }
}