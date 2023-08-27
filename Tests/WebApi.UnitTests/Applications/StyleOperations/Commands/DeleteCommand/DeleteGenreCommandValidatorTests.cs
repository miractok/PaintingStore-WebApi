using FluentAssertions;
using TestSetup;
using WebApi.Application.StyleOperations.Commands.DeleteStyle;

namespace Applications.StyleOperations.Commands.DeleteCommand
{
    public class DeleteStyleCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenStyleIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteStyleCommand command = new DeleteStyleCommand(null);
            command.StyleId = 0;

            //act
            DeleteStyleCommandValidator validator = new DeleteStyleCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenStyleIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteStyleCommand command = new DeleteStyleCommand(null);
            command.StyleId = 1;

            DeleteStyleCommandValidator validator = new DeleteStyleCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}