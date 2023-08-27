using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.PaintingOperations.Commands.CreatePainting;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.PaintingOperations.Commands.CreateCommand
{
    public class CreatePaintingCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreatePaintingCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistPaintingTitleIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            var Painting = new Painting() {Name = "Test_WhenAlreadyExistPaintingTitleIsGiven_InvalidOperationException_ShouldBeReturn"};
            _context.Paintings.Add(Painting);
            _context.SaveChanges();

            CreatePaintingCommand command = new CreatePaintingCommand(_mapper,_context);
            command.Model = new CreatePaintingModel(){ Name = Painting.Name};
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Painting already exists!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Painting_ShouldCreated()
        {
            //arrange
            CreatePaintingCommand command = new CreatePaintingCommand(_mapper,_context);
            CreatePaintingModel model = new CreatePaintingModel(){ Name = "TestPainting9", PublishDate = DateTime.Now.Date.AddYears(-2), ArtistId = 4, MediumId = 4, GenreId = 4, StyleId = 4, Subject = "TestSubject78", Price = 865};
            command.Model = model;
            
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var painting = _context.Paintings.SingleOrDefault(painting => painting.Name == model.Name);

            painting.Should().NotBeNull();
            painting.PublishDate.Should().Be(model.PublishDate);
            painting.ArtistId.Should().Be(model.ArtistId);
            painting.MediumId.Should().Be(model.MediumId);
            painting.GenreId.Should().Be(model.GenreId);
            painting.StyleId.Should().Be(model.StyleId);
            painting.Subject.Should().Be(model.Subject);
            painting.Price.Should().Be(model.Price);
        }

    }
}