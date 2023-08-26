using FluentValidation;

namespace WebApi.Application.ArtistPaintingOperations.Queries.GetArtistPaintingDetails
{
    public class GetArtistPaintingDetailsQueryValidator : AbstractValidator<GetArtistPaintingDetailsQuery>
    {
        public GetArtistPaintingDetailsQueryValidator()
        {
            RuleFor(query => query.ArtistPaintingId).GreaterThan(0);
        }
    }
}