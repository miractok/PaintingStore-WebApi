using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.StyleOperations.Commands.UpdateStyle;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.StyleOperations.Commands.UpdateCommand
{
    public class UpdateStyleCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateStyleCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenStyleIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            UpdateStyleCommand command = new UpdateStyleCommand(_context);
            command.StyleId = 756;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Style does not exist!");
        }

        [Fact]
        public void WhenAlreadyExistStyleNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var Style = new Style() {Name = "Test_WhenAlreadyExistStyleNameIsGiven_InvalidOperationException_ShouldReturn1"};
            _context.Styles.Add(Style);
            _context.SaveChanges();

            UpdateStyleCommand command = new UpdateStyleCommand(_context);
            command.Model = new UpdateStyleModel() {  Name = Style.Name };
            command.StyleId = 1;

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Style already exists!");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Style_ShouldBeUpdated()
        {
            //arrange
            UpdateStyleCommand command = new UpdateStyleCommand(_context);
            UpdateStyleModel model = new UpdateStyleModel() {Name="TestStyleName"};
            command.Model = model;
            command.StyleId = 1;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updateStyle = _context.Styles.SingleOrDefault(Style => Style.Name == model.Name);
            updateStyle.Should().NotBeNull();
        }
    }
}