using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistOperations.Queries.GetArtistDetails;

namespace Applications.ArtistOperations.Queries.GetArtistDetails
{
    public class GetArtistDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetArtistDetailsQuery query = new GetArtistDetailsQuery(null, null);
            query.ArtistId = 0;
            GetArtistDetailsQueryValidator validator = new GetArtistDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetArtistDetailsQuery query = new GetArtistDetailsQuery(null, null);
            query.ArtistId = 1;
            GetArtistDetailsQueryValidator validator = new GetArtistDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}