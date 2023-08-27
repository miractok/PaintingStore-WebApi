using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.MediumOperations.Commands.UpdateMedium;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.MediumOperations.Commands.UpdateCommand
{
    public class UpdateMediumCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateMediumCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenMediumIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            UpdateMediumCommand command = new UpdateMediumCommand(_context);
            command.MediumId = 756;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Medium does not exist!");
        }

        [Fact]
        public void WhenAlreadyExistMediumNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var Medium = new Medium() {Name = "Test_WhenAlreadyExistMediumNameIsGiven_InvalidOperationException_ShouldReturn1"};
            _context.Mediums.Add(Medium);
            _context.SaveChanges();

            UpdateMediumCommand command = new UpdateMediumCommand(_context);
            command.Model = new UpdateMediumModel() {  Name = Medium.Name };
            command.MediumId = 1;

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Medium already exists!");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Medium_ShouldBeUpdated()
        {
            //arrange
            UpdateMediumCommand command = new UpdateMediumCommand(_context);
            UpdateMediumModel model = new UpdateMediumModel() {Name="TestMediumName"};
            command.Model = model;
            command.MediumId = 1;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updateMedium = _context.Mediums.SingleOrDefault(Medium => Medium.Name == model.Name);
            updateMedium.Should().NotBeNull();
        }
    }
}