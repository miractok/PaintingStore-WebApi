using FluentValidation;

namespace WebApi.Application.ArtistPaintingOperations.Commands.DeleteArtistPainting
{
    public class DeleteArtistPaintingCommandValidator : AbstractValidator<DeleteArtistPaintingCommand>
    {
        public DeleteArtistPaintingCommandValidator()
        {
            RuleFor(command => command.DataId).GreaterThan(0);
        }
    }
}