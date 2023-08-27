using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace Applications.GenreOperations.Commands.CreateCommand
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("L")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreViewModel()
            {
                Name = ""
            };

            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreViewModel()
            {
                Name = "GenreNameTest"
            };

            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}