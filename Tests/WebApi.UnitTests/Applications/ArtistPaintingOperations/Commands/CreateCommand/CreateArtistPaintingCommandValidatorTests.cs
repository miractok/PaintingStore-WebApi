using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistPaintingOperations.Commands.CreateArtistPainting;

namespace Applications.ArtistPaintingOperations.Commands.CreateCommand
{
    public class CreateArtistPaintingCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int Artistid, int Paintingid)
        {
            //arrange
            CreateArtistPaintingCommand command = new CreateArtistPaintingCommand(null, null);
            command.Model = new CreateArtistPaintingViewModel()
            {
                ArtistId = 0,
                PaintingId = 0
            };

            //act
            CreateArtistPaintingCommandValidator validator = new CreateArtistPaintingCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            //arrange
            CreateArtistPaintingCommand command = new CreateArtistPaintingCommand(null, null);
            command.Model = new CreateArtistPaintingViewModel()
            {
                ArtistId = 1,
                PaintingId = 1
            };

            //act
            CreateArtistPaintingCommandValidator validator = new CreateArtistPaintingCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}