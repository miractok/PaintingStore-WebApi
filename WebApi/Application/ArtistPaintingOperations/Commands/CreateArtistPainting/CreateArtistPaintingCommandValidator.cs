using FluentValidation;

namespace WebApi.Application.ArtistPaintingOperations.Commands.CreateArtistPainting
{
    public class CreateArtistPaintingCommandValidator : AbstractValidator<CreateArtistPaintingCommand>
    {
        public CreateArtistPaintingCommandValidator()
        {
            RuleFor(command => command.Model.ArtistId).GreaterThan(0);
            RuleFor(command => command.Model.PaintingId).GreaterThan(0);
        }
    }
}