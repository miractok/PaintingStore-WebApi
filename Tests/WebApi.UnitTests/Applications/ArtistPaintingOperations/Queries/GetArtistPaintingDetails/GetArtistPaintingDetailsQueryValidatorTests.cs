using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistPaintingOperations.Queries.GetArtistPaintingDetails;

namespace Applications.ArtistPaintingOperations.Queries.GetArtistPaintingDetails
{
    public class GetArtistPaintingDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetArtistPaintingDetailsQuery query = new GetArtistPaintingDetailsQuery(null, null);
            query.ArtistPaintingId = 0;
            GetArtistPaintingDetailsQueryValidator validator = new GetArtistPaintingDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetArtistPaintingDetailsQuery query = new GetArtistPaintingDetailsQuery(null, null);
            query.ArtistPaintingId = 1;
            GetArtistPaintingDetailsQueryValidator validator = new GetArtistPaintingDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}