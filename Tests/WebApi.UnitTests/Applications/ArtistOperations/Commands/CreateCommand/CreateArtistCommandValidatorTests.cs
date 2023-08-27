using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistOperations.Commands.CreateArtist;

namespace Applications.ArtistOperations.Commands.CreateCommand
{
    public class CreateArtistCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("L")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string namesurname)
        {
            //arrange
            CreateArtistCommand command = new CreateArtistCommand(null, null);
            command.Model = new CreateArtistViewModel()
            {
                NameSurname = ""
            };

            //act
            CreateArtistCommandValidator validator = new CreateArtistCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateArtistCommand command = new CreateArtistCommand(null, null);
            command.Model = new CreateArtistViewModel()
            {
                NameSurname = "ArtistNameTest",
                Country = "CountryTest"
            };

            //act
            CreateArtistCommandValidator validator = new CreateArtistCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}