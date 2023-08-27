using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.PaintingOperations.Commands.UpdatePainting;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.PaintingOperations.Commands.UpdateCommand
{
    public class UpdatePaintingCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdatePaintingCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var Painting = new Painting() {Name = "Test_WhenInvalidIdIsGiven_InvalidOperationException_ShouldReturn", PublishDate = new DateTime(1957,08,12),ArtistId = 2, MediumId = 1, StyleId = 3, GenreId = 3, Subject = "TestSubject3", Price = 25};
            _context.Paintings.Add(Painting);
            _context.SaveChanges();

            UpdatePaintingCommand command = new UpdatePaintingCommand(_context);
            command.Model = new UpdatePaintingModel() { Name = Painting.Name };

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Painting could not be found!");
            
        }

        [Fact]
        public void WhenAlreadyExistPaintingNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var Painting = new Painting() {Name = "Test_WhenAlreadyExistPaintingNameIsGiven_InvalidOperationException_ShouldReturn1", PublishDate = new DateTime(1957,08,12), ArtistId = 3, MediumId = 2, StyleId = 1, GenreId = 3, Subject = "TestSubject9", Price = 59};
            _context.Paintings.Add(Painting);
            _context.SaveChanges();

            UpdatePaintingCommand command = new UpdatePaintingCommand(_context);
            command.Model = new UpdatePaintingModel() { Name = Painting.Name };
            command.PaintingId = 1;

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Painting already exists!");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Painting_ShouldBeUpdated()
        {
            //arrange
            UpdatePaintingCommand command = new UpdatePaintingCommand(_context);
            UpdatePaintingModel model = new UpdatePaintingModel() {Name="TestPaintingName",PublishDate = DateTime.Now.Date.AddYears(-69),ArtistId = 3, MediumId = 1, StyleId = 3 , GenreId = 2, Subject = "TestSubject26", Price = 65};
            command.Model = model;
            command.PaintingId = 1;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updatePainting = _context.Paintings.SingleOrDefault(Painting => Painting.Name == model.Name);
            updatePainting.Should().NotBeNull();
            updatePainting.PublishDate.Should().Be(model.PublishDate);
            updatePainting.GenreId.Should().Be(model.GenreId);
            updatePainting.Price.Should().Be(model.Price);
        }
    }
}