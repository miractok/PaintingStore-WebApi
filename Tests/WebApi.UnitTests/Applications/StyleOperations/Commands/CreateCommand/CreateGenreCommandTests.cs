using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.StyleOperations.Commands.CreateStyle;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.StyleOperations.Commands.CreateCommand
{
    public class CreateStyleCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateStyleCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistStyleNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            var Style = new Style() {Name = "Test_WhenAlreadyExistStyleNameIsGiven_InvalidOperationException_ShouldReturn"};
            _context.Styles.Add(Style);
            _context.SaveChanges();

            CreateStyleCommand command = new CreateStyleCommand(_mapper,_context);
            command.Model = new CreateStyleViewModel(){ Name = Style.Name};
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This Style already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Style_ShouldCreated()
        {
            //arrange
            CreateStyleCommand command = new CreateStyleCommand(_mapper,_context);
            CreateStyleViewModel model = new CreateStyleViewModel(){ Name = "TestStyle"};
            command.Model = model;
            
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var Style = _context.Styles.SingleOrDefault(Style => Style.Name == model.Name);

            Style.Should().NotBeNull();
        }

    }
}