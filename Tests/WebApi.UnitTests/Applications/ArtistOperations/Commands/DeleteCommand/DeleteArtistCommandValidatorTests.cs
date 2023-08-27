using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistOperations.Commands.DeleteArtist;

namespace Applications.ArtistOperations.Commands.DeleteCommand
{
    public class DeleteArtistCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenArtistIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteArtistCommand command = new DeleteArtistCommand(null);
            command.ArtistId = 0;

            //act
            DeleteArtistCommandValidator validator = new DeleteArtistCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenArtistIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteArtistCommand command = new DeleteArtistCommand(null);
            command.ArtistId = 1;

            DeleteArtistCommandValidator validator = new DeleteArtistCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}