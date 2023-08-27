using FluentAssertions;
using TestSetup;
using WebApi.Application.MediumOperations.Commands.DeleteMedium;

namespace Applications.MediumOperations.Commands.DeleteCommand
{
    public class DeleteMediumCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenMediumIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteMediumCommand command = new DeleteMediumCommand(null);
            command.MediumId = 0;

            //act
            DeleteMediumCommandValidator validator = new DeleteMediumCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenMediumIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteMediumCommand command = new DeleteMediumCommand(null);
            command.MediumId = 1;

            DeleteMediumCommandValidator validator = new DeleteMediumCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}