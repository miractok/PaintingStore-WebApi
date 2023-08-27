using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.MediumOperations.Commands.CreateMedium;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.MediumOperations.Commands.CreateCommand
{
    public class CreateMediumCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMediumCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistMediumNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            var Medium = new Medium() {Name = "Test_WhenAlreadyExistMediumNameIsGiven_InvalidOperationException_ShouldReturn"};
            _context.Mediums.Add(Medium);
            _context.SaveChanges();

            CreateMediumCommand command = new CreateMediumCommand(_mapper,_context);
            command.Model = new CreateMediumViewModel(){ Name = Medium.Name};
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This medium already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Medium_ShouldCreated()
        {
            //arrange
            CreateMediumCommand command = new CreateMediumCommand(_mapper,_context);
            CreateMediumViewModel model = new CreateMediumViewModel(){ Name = "TestMedium"};
            command.Model = model;
            
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var medium = _context.Mediums.SingleOrDefault(medium => medium.Name == model.Name);

            medium.Should().NotBeNull();
        }

    }
}