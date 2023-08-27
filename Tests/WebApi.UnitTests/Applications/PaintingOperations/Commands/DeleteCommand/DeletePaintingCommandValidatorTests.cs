using FluentAssertions;
using TestSetup;
using WebApi.Application.PaintingOperations.Commands.DeletePainting;

namespace Applications.PaintingOperations.Commands.DeleteCommand
{
    public class DeletePaintingCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenPaintingIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeletePaintingCommand command = new DeletePaintingCommand(null);
            command.PaintingId = 0;

            //act
            DeletePaintingCommandValidator validator = new DeletePaintingCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPaintingIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeletePaintingCommand command = new DeletePaintingCommand(null);
            command.PaintingId = 1;

            DeletePaintingCommandValidator validator = new DeletePaintingCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}