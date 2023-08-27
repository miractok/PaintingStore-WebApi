using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.StyleOperations.Queries.GetStyleDetails;
using WebApi.DBOperations;

namespace Applications.StyleOperations.Queries.GetStyleDetails
{
    public class GetStyleDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly PaintingStoreDbContext _context;
        readonly IMapper _mapper;

        public GetStyleDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongStyleIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetStyleDetailsQuery query = new GetStyleDetailsQuery(_context, _mapper);
            query.StyleId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any Style.");
        }

        [Fact]
        public void WhenValidStyleIdIsGiven_Style_ShouldReturn()
        {
            GetStyleDetailsQuery query = new GetStyleDetailsQuery(_context, _mapper);
            query.StyleId = 3;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var Style = _context.Styles.SingleOrDefault(Style => Style.Id == query.StyleId);
            Style.Should().NotBeNull();
        }
    }
}