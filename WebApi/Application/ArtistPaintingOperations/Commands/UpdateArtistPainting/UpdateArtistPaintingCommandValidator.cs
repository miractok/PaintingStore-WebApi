using FluentValidation;

namespace WebApi.Application.ArtistPaintingOperations.Commands.UpdateArtistPainting
{
    public class UpdateArtistPaintingCommandValidator : AbstractValidator<UpdateArtistPaintingCommand>
    {
        public UpdateArtistPaintingCommandValidator()
        {
            RuleFor(command => command.Model.PaintingId).GreaterThan(0);
            RuleFor(command => command.Model.ArtistId).GreaterThan(0);
        }
    }
}