using FluentAssertions;
using TestSetup;
using WebApi.Application.MediumOperations.Commands.DeleteMedium;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.MediumOperations.Commands.DeleteCommand
{
    public class DeleteMediumCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly PaintingStoreDbContext _context;
        public DeleteMediumCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenMediumIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteMediumCommand command = new DeleteMediumCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Medium does not exist!");
        }
        //Happy Case
        [Fact]
        public void WhenMediumIdIsValid_Film_ShouldBeDeleted()
        {
            var Medium = new Medium() {Name = "Test_WhenMediumIdIsValid_Film_ShouldBeDeleted"};
            _context.Mediums.Add(Medium);
            _context.SaveChanges();

            DeleteMediumCommand command = new DeleteMediumCommand(_context);    
            command.MediumId = Medium.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var mediumCheck = _context.Mediums.SingleOrDefault(mediumCheck=> mediumCheck.Id == Medium.Id);
            mediumCheck.Should().BeNull();
        }
    }
}