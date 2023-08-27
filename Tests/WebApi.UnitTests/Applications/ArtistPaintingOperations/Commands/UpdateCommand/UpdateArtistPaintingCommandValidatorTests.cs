using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistPaintingOperations.Commands.UpdateArtistPainting;

namespace Applications.ArtistPaintingOperations.Commands.UpdateCommand
{
    public class UpdateArtistPaintingCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int Artistid, int Paintingid)
        {
            //arrange
            UpdateArtistPaintingCommand command = new UpdateArtistPaintingCommand(null);
            command.Model = new UpdateArtistPaintingModel()
            {
                ArtistId = Artistid,
                PaintingId = Paintingid
            };

            //act
           UpdateArtistPaintingCommandValidator validator = new UpdateArtistPaintingCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var dataid =1;
            UpdateArtistPaintingCommand command = new UpdateArtistPaintingCommand(null);
            command.DataId = dataid;
            command.Model = new UpdateArtistPaintingModel()
            {
                ArtistId = 4,
                PaintingId = 4
            };

            UpdateArtistPaintingCommandValidator validator = new UpdateArtistPaintingCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}