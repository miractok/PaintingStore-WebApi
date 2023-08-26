using FluentValidation;

namespace WebApi.Application.ArtistOperations.Commands.CreateArtist
{
    public class CreateArtistCommandValidator : AbstractValidator<CreateArtistCommand>
    {
        public CreateArtistCommandValidator()
        {
            RuleFor(command => command.Model.NameSurname).NotEmpty().MinimumLength(1).NotEqual("string");
            RuleFor(command => command.Model.Country).NotEmpty().NotNull().MinimumLength(1).NotEqual("string");

        }
    }
}