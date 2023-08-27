using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistOperations.Commands.UpdateArtist;

namespace Applications.ArtistOperations.Commands.UpdateCommand
{
    public class UpdateArtistCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string namesurname)
        {
            //arrange
            UpdateArtistCommand command = new UpdateArtistCommand(null);
            command.Model = new UpdateArtistModel()
            {
                NameSurname = namesurname
            };

            //act
           UpdateArtistCommandValidator validator = new UpdateArtistCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var Artistid =1;
            UpdateArtistCommand command = new UpdateArtistCommand(null);
            command.ArtistId = Artistid;
            command.Model = new UpdateArtistModel()
            {
                NameSurname = "TestArtistName",
                Country = "TestCountry"
            };

            UpdateArtistCommandValidator validator = new UpdateArtistCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}