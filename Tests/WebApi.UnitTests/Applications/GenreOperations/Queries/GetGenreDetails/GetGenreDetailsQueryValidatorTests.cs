using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;

namespace Applications.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetGenreDetailsQuery query = new GetGenreDetailsQuery(null, null);
            query.GenreId = 0;
            GetGenreDetailsQueryValidator validator = new GetGenreDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetGenreDetailsQuery query = new GetGenreDetailsQuery(null, null);
            query.GenreId = 1;
            GetGenreDetailsQueryValidator validator = new GetGenreDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}