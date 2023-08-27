using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace Applications.GenreOperations.Commands.UpdateCommand
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel()
            {
                Name = name
            };

            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var genreid =1;
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreid;
            command.Model = new UpdateGenreModel()
            {
                Name = "TestGenreName"
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}