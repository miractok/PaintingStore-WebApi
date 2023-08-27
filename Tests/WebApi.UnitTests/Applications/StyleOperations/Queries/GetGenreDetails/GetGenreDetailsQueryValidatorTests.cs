using FluentAssertions;
using TestSetup;
using WebApi.Application.StyleOperations.Queries.GetStyleDetails;

namespace Applications.StyleOperations.Queries.GetStyleDetails
{
    public class GetStyleDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetStyleDetailsQuery query = new GetStyleDetailsQuery(null, null);
            query.StyleId = 0;
            GetStyleDetailsQueryValidator validator = new GetStyleDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetStyleDetailsQuery query = new GetStyleDetailsQuery(null, null);
            query.StyleId = 1;
            GetStyleDetailsQueryValidator validator = new GetStyleDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}