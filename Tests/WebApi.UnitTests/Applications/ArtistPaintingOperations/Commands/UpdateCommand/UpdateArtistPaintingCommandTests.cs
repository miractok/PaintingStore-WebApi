using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistPaintingOperations.Commands.UpdateArtistPainting;
using WebApi.DBOperations;

namespace Applications.ArtistPaintingOperations.Commands.UpdateCommand
{
    public class UpdateArtistPaintingCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateArtistPaintingCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDataIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            UpdateArtistPaintingCommand command = new UpdateArtistPaintingCommand(_context);
            command.DataId = 789;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Data could not be found!");
        }

        [Fact]
        public void WhenWrongArtistIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            UpdateArtistPaintingModel model = new UpdateArtistPaintingModel() { ArtistId = 782, PaintingId = 1};

            //act
            UpdateArtistPaintingCommand command = new UpdateArtistPaintingCommand(_context);
            command.Model = model;
            command.DataId = 2;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Artist could not be found!");
        }

        [Fact]
        public void WhenWrongPaintingIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            UpdateArtistPaintingModel model = new UpdateArtistPaintingModel() { ArtistId = 1, PaintingId = 487};

            //act
            UpdateArtistPaintingCommand command = new UpdateArtistPaintingCommand(_context);
            command.Model = model;
            command.DataId = 3;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Painting could not be found!");
        }

        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_ArtistPaintingRelation_ShouldBeUpdated()
        {
            //arrange
            UpdateArtistPaintingCommand command = new UpdateArtistPaintingCommand(_context);
            UpdateArtistPaintingModel model = new UpdateArtistPaintingModel() {ArtistId = 1, PaintingId = 3};
            command.Model = model;
            command.DataId = 3;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updateArtistPainting = _context.ArtistPaintings.SingleOrDefault(artistPainting => artistPainting.ArtistId == model.ArtistId && artistPainting.PaintingId == model.PaintingId);
            updateArtistPainting.Should().NotBeNull();
        }
    }
}