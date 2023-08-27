using FluentAssertions;
using TestSetup;
using WebApi.Application.StyleOperations.Commands.DeleteStyle;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.StyleOperations.Commands.DeleteCommand
{
    public class DeleteStyleCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        public DeleteStyleCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenStyleIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteStyleCommand command = new DeleteStyleCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Style does not exist!");
        }
        //Happy Case
        [Fact]
        public void WhenStyleIdIsValid_Film_ShouldBeDeleted()
        {
            var Style = new Style() {Name = "Test_WhenStyleIdIsValid_Film_ShouldBeDeleted"};
            _context.Styles.Add(Style);
            _context.SaveChanges();

            DeleteStyleCommand command = new DeleteStyleCommand(_context);    
            command.StyleId = Style.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var StyleCheck = _context.Styles.SingleOrDefault(StyleCheck=> StyleCheck.Id == Style.Id);
            StyleCheck.Should().BeNull();
        }
    }
}