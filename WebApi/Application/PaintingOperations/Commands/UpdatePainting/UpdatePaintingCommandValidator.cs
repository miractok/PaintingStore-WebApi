using FluentValidation;

namespace WebApi.Application.PaintingOperations.Commands.UpdatePainting
{
    public class UpdatePaintingCommandValidator : AbstractValidator<UpdatePaintingCommand>
    {
        public UpdatePaintingCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().NotNull().MinimumLength(1).NotEqual("string");
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.ArtistId).GreaterThan(0);
            RuleFor(command => command.Model.StyleId).GreaterThan(0);
            RuleFor(command => command.Model.MediumId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.Subject).NotEmpty().NotNull().MinimumLength(1).NotEqual("string");
            RuleFor(command => command.Model.Price).NotEmpty().GreaterThan(0);
        }
    }
}