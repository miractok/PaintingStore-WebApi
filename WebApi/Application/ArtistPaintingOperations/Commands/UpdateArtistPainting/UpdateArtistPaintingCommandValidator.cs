using FluentValidation;

namespace WebApi.Application.ArtistPaintingOperations.Commands.UpdateArtistPainting
{
    public class UpdateArtistPaintingCommandValidator : AbstractValidator<UpdateArtistPaintingCommand>
    {
        public UpdateArtistPaintingCommandValidator()
        {
            RuleFor(command => command.Model.FilmId).GreaterThan(0);
            RuleFor(command => command.Model.ActorId).GreaterThan(0);
        }
    }
}