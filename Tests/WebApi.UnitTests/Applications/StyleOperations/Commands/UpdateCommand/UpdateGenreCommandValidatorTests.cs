using FluentAssertions;
using TestSetup;
using WebApi.Application.StyleOperations.Commands.UpdateStyle;

namespace Applications.StyleOperations.Commands.UpdateCommand
{
    public class UpdateStyleCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            UpdateStyleCommand command = new UpdateStyleCommand(null);
            command.Model = new UpdateStyleModel()
            {
                Name = name
            };

            //act
            UpdateStyleCommandValidator validator = new UpdateStyleCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var Styleid =1;
            UpdateStyleCommand command = new UpdateStyleCommand(null);
            command.StyleId = Styleid;
            command.Model = new UpdateStyleModel()
            {
                Name = "TestStyleName"
            };

            UpdateStyleCommandValidator validator = new UpdateStyleCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}