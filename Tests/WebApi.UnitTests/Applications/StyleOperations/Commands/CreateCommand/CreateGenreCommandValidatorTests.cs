using FluentAssertions;
using TestSetup;
using WebApi.Application.StyleOperations.Commands.CreateStyle;

namespace Applications.StyleOperations.Commands.CreateCommand
{
    public class CreateStyleCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("L")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
        {
            //arrange
            CreateStyleCommand command = new CreateStyleCommand(null, null);
            command.Model = new CreateStyleViewModel()
            {
                Name = ""
            };

            //act
            CreateStyleCommandValidator validator = new CreateStyleCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateStyleCommand command = new CreateStyleCommand(null, null);
            command.Model = new CreateStyleViewModel()
            {
                Name = "StyleNameTest"
            };

            //act
            CreateStyleCommandValidator validator = new CreateStyleCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}