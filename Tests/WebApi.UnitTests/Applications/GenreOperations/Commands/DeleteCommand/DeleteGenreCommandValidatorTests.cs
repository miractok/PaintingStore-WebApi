using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace Applications.GenreOperations.Commands.DeleteCommand
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenGenreIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 0;

            //act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGenreIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 1;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}