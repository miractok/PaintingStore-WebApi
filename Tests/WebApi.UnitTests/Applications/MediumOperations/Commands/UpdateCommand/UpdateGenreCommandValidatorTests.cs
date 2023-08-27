using FluentAssertions;
using TestSetup;
using WebApi.Application.MediumOperations.Commands.UpdateMedium;

namespace Applications.MediumOperations.Commands.UpdateCommand
{
    public class UpdateMediumCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            UpdateMediumCommand command = new UpdateMediumCommand(null);
            command.Model = new UpdateMediumModel()
            {
                Name = name
            };

            //act
            UpdateMediumCommandValidator validator = new UpdateMediumCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var Mediumid =1;
            UpdateMediumCommand command = new UpdateMediumCommand(null);
            command.MediumId = Mediumid;
            command.Model = new UpdateMediumModel()
            {
                Name = "TestMediumName"
            };

            UpdateMediumCommandValidator validator = new UpdateMediumCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}