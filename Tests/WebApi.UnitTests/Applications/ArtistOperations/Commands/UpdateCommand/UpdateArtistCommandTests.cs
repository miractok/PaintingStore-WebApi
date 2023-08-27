using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistOperations.Commands.UpdateArtist;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ArtistOperations.Commands.UpdateCommand
{
    public class UpdateArtistCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateArtistCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenArtistIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            UpdateArtistCommand command = new UpdateArtistCommand(_context);
            command.ArtistId = 426;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Artist could not be found!");
        }

        [Fact]
        public void WhenAlreadyExistArtistNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var Artist = new Artist() {NameSurname = "Test_WhenAlreadyExistArtistNameIsGiven_InvalidOperationException_ShouldReturn1"};
            _context.Artists.Add(Artist);
            _context.SaveChanges();

            UpdateArtistCommand command = new UpdateArtistCommand(_context);
            command.Model = new UpdateArtistModel() {  NameSurname = Artist.NameSurname };
            command.ArtistId = 1;

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Artist already exists!");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Artist_ShouldBeUpdated()
        {
            //arrange
            UpdateArtistCommand command = new UpdateArtistCommand(_context);
            UpdateArtistModel model = new UpdateArtistModel() {NameSurname="TestArtistName"};
            command.Model = model;
            command.ArtistId = 1;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updateArtist = _context.Artists.SingleOrDefault(Artist => Artist.NameSurname == model.NameSurname);
            updateArtist.Should().NotBeNull();
        }
    }
}