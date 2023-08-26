using FluentValidation;

namespace WebApi.Application.ArtistOperations.Commands.DeleteArtist
{
    public class DeleteArtistCommandValidator : AbstractValidator<DeleteArtistCommand>
    {
        public DeleteArtistCommandValidator()
        {
            RuleFor(command => command.ArtistId).GreaterThan(0);
        }
    }
}