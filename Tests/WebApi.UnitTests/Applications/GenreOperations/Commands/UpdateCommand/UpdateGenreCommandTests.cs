using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.GenreOperations.Commands.UpdateCommand
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGenreIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 756;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Genre does not exist!");
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var genre = new Genre() {Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldReturn1"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = new UpdateGenreModel() {  Name = genre.Name };
            command.GenreId = 1;

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre already exists!");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel() {Name="TestGenreName"};
            command.Model = model;
            command.GenreId = 1;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updategenre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);
            updategenre.Should().NotBeNull();
        }
    }
}