using FluentAssertions;
using TestSetup;
using WebApi.Application.PaintingOperations.Commands.DeletePainting;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.PaintingOperations.Commands.DeleteCommand
{
    public class DeletePaintingCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        public DeletePaintingCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenPaintingIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeletePaintingCommand command = new DeletePaintingCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Painting could not be found!");
        }
        //Happy Case
        [Fact]
        public void WhenPaintingIdIsValid_Painting_ShouldBeDeleted()
        {
            var Painting = new Painting() {Name = "Test_WhenPaintingIdIsValid_Painting_ShouldBeDeleted", PublishDate = new DateTime(2001,08,12),ArtistId = 1, MediumId = 1, StyleId = 1, GenreId = 1, Subject = "TestSubject8", Price = 10};
            _context.Paintings.Add(Painting);
            _context.SaveChanges();

            DeletePaintingCommand command = new DeletePaintingCommand(_context);    
            command.PaintingId = Painting.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var PaintingCheck = _context.Paintings.SingleOrDefault(PaintingCheck=> PaintingCheck.Id == Painting.Id);
            PaintingCheck.Should().BeNull();
        }
    }
}