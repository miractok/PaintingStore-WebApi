using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistPaintingOperations.Commands.CreateArtistPainting;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ArtistPaintingOperations.Commands.CreateCommand
{
    public class CreateArtistPaintingCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateArtistPaintingCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongArtistIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            CreateArtistPaintingViewModel model = new CreateArtistPaintingViewModel() { ArtistId = 565, PaintingId = 1};

            //act
            CreateArtistPaintingCommand command = new CreateArtistPaintingCommand(_mapper,_context);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Artist could not be found!");
        }

        [Fact]
        public void WhenWrongPaintingIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            CreateArtistPaintingViewModel model = new CreateArtistPaintingViewModel() { ArtistId = 1, PaintingId = 465};

            //act
            CreateArtistPaintingCommand command = new CreateArtistPaintingCommand(_mapper,_context);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Painting could not be found!");
        }

        [Fact]
        public void WhenAlreadyExistPaintingAndArtistIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            var artistPainting = new ArtistPainting() {ArtistId = 3 , PaintingId = 4};
            _context.ArtistPaintings.Add(artistPainting);
            _context.SaveChanges();

            CreateArtistPaintingViewModel model = new CreateArtistPaintingViewModel() { ArtistId = 3, PaintingId = 4};

            //act
            CreateArtistPaintingCommand command = new CreateArtistPaintingCommand(_mapper,_context);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("This relation already exists.");
        }

        [Fact]
        public void WhenNotExistPaintingndArtistIdIsGiven_ArtistPaintingRelation_ShouldBeCreated()
        {
            // arrange
            CreateArtistPaintingViewModel model = new CreateArtistPaintingViewModel() { ArtistId = 4, PaintingId = 4};
            CreateArtistPaintingCommand command = new CreateArtistPaintingCommand(_mapper,_context);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var artistPaintings = _context.ArtistPaintings.SingleOrDefault(s => s.ArtistId == model.ArtistId && s.PaintingId == model.PaintingId);
            
            artistPaintings.Should().NotBeNull();
        }
    }
}