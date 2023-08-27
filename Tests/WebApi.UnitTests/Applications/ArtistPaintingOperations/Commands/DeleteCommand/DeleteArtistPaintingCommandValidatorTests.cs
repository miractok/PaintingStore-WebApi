using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistPaintingOperations.Commands.DeleteArtistPainting;

namespace Applications.ArtistPaintingOperations.Commands.DeleteCommand
{
    public class DeleteArtistPaintingCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenDataIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteArtistPaintingCommand command = new DeleteArtistPaintingCommand(null);
            command.DataId = 0;

            //act
            DeleteArtistPaintingCommandValidator validator = new DeleteArtistPaintingCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDataIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteArtistPaintingCommand command = new DeleteArtistPaintingCommand(null);
            command.DataId = 1;

            DeleteArtistPaintingCommandValidator validator = new DeleteArtistPaintingCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}