using FluentAssertions;
using TestSetup;
using WebApi.Application.PaintingOperations.Commands.CreatePainting;

namespace Applications.PaintingOperations.Commands.CreateCommand
{
    public class CreatePaintingCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("TestName", 0, 0, 0, 0)]
        [InlineData("TestName", 1, 0, 0, 0)]
        [InlineData("TestName", 0, 1, 0, 0)]
        [InlineData("TestName", 0, 0, 1, 0)]
        [InlineData("TestName", 0, 0, 0, 1)]
        [InlineData("", 0, 0, 0, 0)]
        [InlineData("", 1, 0, 0, 0)]
        [InlineData("", 0, 1, 0, 0)]
        [InlineData("", 0, 0, 1, 0)]
        [InlineData("", 0, 0, 0, 1)]
        [InlineData("L", 0, 0, 0, 0)]
        [InlineData("L", 1, 0, 0, 0)]
        [InlineData("L", 0, 1, 0, 0)]
        [InlineData("L", 0, 0, 1, 0)]
        [InlineData("L", 0, 0, 0, 1)]
        [InlineData(" ", 1, 1, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, int mediumId, int styleId, int genreId, int price)
        {
            //arrange
            CreatePaintingCommand command = new CreatePaintingCommand(null, null);
            command.Model = new CreatePaintingModel()
            {
                Name = "",
                PublishDate = DateTime.Now.Date,
                MediumId = 0,
                StyleId = 0,
                GenreId = 0,
                Price = 0
            };

            //act
            CreatePaintingCommandValidator validator = new CreatePaintingCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError()
        {
            CreatePaintingCommand command = new CreatePaintingCommand(null, null);
            command.Model = new CreatePaintingModel()
            {
                Name = "TestName",
                PublishDate = DateTime.Now.Date,
                MediumId = 1,
                StyleId = 1,
                GenreId = 1,
                Price = 102
            };  

            CreatePaintingCommandValidator validator = new CreatePaintingCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreatePaintingCommand command = new CreatePaintingCommand(null, null);
            command.Model = new CreatePaintingModel()
            {
                Name = "TestName",
                PublishDate = DateTime.Now.Date.AddYears(-2),
                ArtistId = 1,
                MediumId = 1,
                StyleId = 1,
                GenreId = 1,
                Subject = "TestSubject",
                Price = 102
            };

            //act
            CreatePaintingCommandValidator validator = new CreatePaintingCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}