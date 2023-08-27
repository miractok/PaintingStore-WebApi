using FluentAssertions;
using TestSetup;
using WebApi.Application.PaintingOperations.Queries.GetPaintingDetails;

namespace Applications.PaintingOperations.Queries.GetPaintingDetails
{
    public class GetPaintingDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetPaintingDetailsQuery query = new GetPaintingDetailsQuery(null, null);
            query.PaintingId = 0;
            GetPaintingDetailsQueryValidator validator = new GetPaintingDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetPaintingDetailsQuery query = new GetPaintingDetailsQuery(null, null);
            query.PaintingId = 1;
            GetPaintingDetailsQueryValidator validator = new GetPaintingDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}