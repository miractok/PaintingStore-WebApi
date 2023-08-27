using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.PaintingOperations.Queries.GetPaintingDetails;
using WebApi.DBOperations;

namespace Applications.PaintingOperations.Queries.GetPaintingDetails
{
    public class GetPaintingDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly PaintingStoreDbContext _context;
        readonly IMapper _mapper;

        public GetPaintingDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongPaintingIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetPaintingDetailsQuery query = new GetPaintingDetailsQuery(_context, _mapper);
            query.PaintingId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any painting.");
        }

        [Fact]
        public void WhenValidPaintingIdIsGiven_Painting_ShouldReturn()
        {
            GetPaintingDetailsQuery query = new GetPaintingDetailsQuery(_context, _mapper);
            query.PaintingId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var painting = _context.Paintings.SingleOrDefault(painting => painting.Id == 1);
            painting.Should().NotBeNull();
        }
    }
}