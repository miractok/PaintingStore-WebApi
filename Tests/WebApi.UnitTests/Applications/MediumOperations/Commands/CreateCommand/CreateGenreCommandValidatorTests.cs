using FluentAssertions;
using TestSetup;
using WebApi.Application.MediumOperations.Commands.CreateMedium;

namespace Applications.MediumOperations.Commands.CreateCommand
{
    public class CreateMediumCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("L")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
        {
            //arrange
            CreateMediumCommand command = new CreateMediumCommand(null, null);
            command.Model = new CreateMediumViewModel()
            {
                Name = ""
            };

            //act
            CreateMediumCommandValidator validator = new CreateMediumCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateMediumCommand command = new CreateMediumCommand(null, null);
            command.Model = new CreateMediumViewModel()
            {
                Name = "MediumNameTest"
            };

            //act
            CreateMediumCommandValidator validator = new CreateMediumCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}