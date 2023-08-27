using FluentAssertions;
using TestSetup;
using WebApi.Application.MediumOperations.Queries.GetMediumDetails;

namespace Applications.MediumOperations.Queries.GetMediumDetails
{
    public class GetMediumDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetMediumDetailsQuery query = new GetMediumDetailsQuery(null, null);
            query.MediumId = 0;
            GetMediumDetailsQueryValidator validator = new GetMediumDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetMediumDetailsQuery query = new GetMediumDetailsQuery(null, null);
            query.MediumId = 1;
            GetMediumDetailsQueryValidator validator = new GetMediumDetailsQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}