using FluentValidation;

namespace WebApi.Application.ArtistOperations.Queries.GetArtistDetails
{
    public class GetArtistDetailsQueryValidator : AbstractValidator<GetArtistDetailsQuery>
    {
        public GetArtistDetailsQueryValidator()
        {
            RuleFor(query => query.ArtistId).GreaterThan(0);
        }
    }
}