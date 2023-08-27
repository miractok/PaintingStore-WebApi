using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistOperations.Commands.DeleteArtist;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ArtistOperations.Commands.DeleteCommand
{
    public class DeleteArtistCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        public DeleteArtistCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenArtistIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteArtistCommand command = new DeleteArtistCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Artist could not be found!");
        }
        //Happy Case
        [Fact]
        public void WhenArtistIdIsValid_Film_ShouldBeDeleted()
        {
            var artist = new Artist() {NameSurname = "Test_WhenArtistIdIsValid_Film_ShouldBeDeleted"};
            _context.Artists.Add(artist);
            _context.SaveChanges();

            DeleteArtistCommand command = new DeleteArtistCommand(_context);    
            command.ArtistId = artist.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var artistCheck = _context.Artists.SingleOrDefault(artistCheck=> artistCheck.Id == artist.Id);
            artistCheck.Should().BeNull();
        }
    }
}