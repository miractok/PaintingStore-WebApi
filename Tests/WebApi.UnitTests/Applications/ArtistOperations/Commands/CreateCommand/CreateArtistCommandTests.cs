using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistOperations.Commands.CreateArtist;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ArtistOperations.Commands.CreateCommand
{
    public class CreateArtistCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateArtistCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistArtistNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            var artist = new Artist() {NameSurname = "Test_WhenAlreadyExistArtistNameIsGiven_InvalidOperationException_ShouldReturn"};
            _context.Artists.Add(artist);
            _context.SaveChanges();

            CreateArtistCommand command = new CreateArtistCommand(_mapper,_context);
            command.Model = new CreateArtistViewModel(){ NameSurname = artist.NameSurname};
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This artist already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Artist_ShouldCreated()
        {
            //arrange
            CreateArtistCommand command = new CreateArtistCommand(_mapper,_context);
            CreateArtistViewModel model = new CreateArtistViewModel(){ NameSurname = "TestArtistNameSurname"};
            command.Model = model;
            
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var artist = _context.Artists.SingleOrDefault(artist => artist.NameSurname == model.NameSurname);

            artist.Should().NotBeNull();
        }

    }
}