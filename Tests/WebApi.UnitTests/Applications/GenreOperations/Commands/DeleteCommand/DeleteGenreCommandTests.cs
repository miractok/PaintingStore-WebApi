using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.GenreOperations.Commands.DeleteCommand
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGenreIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Genre does not exist!");
        }
        //Happy Case
        [Fact]
        public void WhenGenreIdIsValid_Film_ShouldBeDeleted()
        {
            var genre = new Genre() {Name = "Test_WhenGenreIdIsValid_Film_ShouldBeDeleted"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);    
            command.GenreId = genre.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var genreCheck = _context.Genres.SingleOrDefault(genreCheck=> genreCheck.Id == genre.Id);
            genreCheck.Should().BeNull();
        }
    }
}