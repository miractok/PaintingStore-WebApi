using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.GenreOperations.Commands.CreateCommand
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            var genre = new Genre() {Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldReturn"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_mapper,_context);
            command.Model = new CreateGenreViewModel(){ Name = genre.Name};
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This Genre already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldCreated()
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_mapper,_context);
            CreateGenreViewModel model = new CreateGenreViewModel(){ Name = "TestGenre"};
            command.Model = model;
            
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);

            genre.Should().NotBeNull();
        }

    }
}