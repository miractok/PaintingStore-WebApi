using FluentValidation;

namespace WebApi.Application.ArtistOperations.Commands.UpdateArtist
{
    public class UpdateArtistCommandValidator : AbstractValidator<UpdateArtistCommand>
    {
        public UpdateArtistCommandValidator()
        {
            RuleFor(command => command.ArtistId).GreaterThan(0);
            RuleFor(command => command.Model.NameSurname).MinimumLength(1).NotEmpty().NotEqual("string");
            RuleFor(command => command.Model.Country).NotEmpty().NotNull().MinimumLength(1).NotEqual("string");
        }
    }
}