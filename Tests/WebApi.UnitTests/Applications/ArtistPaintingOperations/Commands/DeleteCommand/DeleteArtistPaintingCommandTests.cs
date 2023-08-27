using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistPaintingOperations.Commands.DeleteArtistPainting;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ArtistPaintingOperations.Commands.DeleteCommand
{
    public class DeleteArtistPaintingCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        public DeleteArtistPaintingCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenDataIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteArtistPaintingCommand command = new DeleteArtistPaintingCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Data could not be found!");
        }
        //Happy Case
        [Fact]
        public void WhenDataIdIsValid_Painting_ShouldBeDeleted()
        {
            var ArtistPainting = new ArtistPainting() {ArtistId = 2, PaintingId = 4};
            _context.ArtistPaintings.Add(ArtistPainting);
            _context.SaveChanges();

            DeleteArtistPaintingCommand command = new DeleteArtistPaintingCommand(_context);    
            command.DataId = ArtistPainting.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var dataCheck = _context.ArtistPaintings.SingleOrDefault(dataCheck=> dataCheck.Id == ArtistPainting.Id);
            dataCheck.Should().BeNull();
        }
    }
}