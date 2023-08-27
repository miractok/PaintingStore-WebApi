using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistPaintingOperations.Queries.GetArtistPaintingDetails;
using WebApi.DBOperations;

namespace Applications.ArtistPaintingOperations.Queries.GetArtistPaintingDetails
{
    public class GetArtistPaintingDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly PaintingStoreDbContext _context;
        readonly IMapper _mapper;

        public GetArtistPaintingDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongArtistIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetArtistPaintingDetailsQuery query = new GetArtistPaintingDetailsQuery(_context, _mapper);
            query.ArtistPaintingId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any ArtistPainting relation.");
        }

        [Fact]
        public void WhenValidArtistPaintingIdIsGiven_ArtistPainting_ShouldReturn()
        {
            GetArtistPaintingDetailsQuery query = new GetArtistPaintingDetailsQuery(_context, _mapper);
            query.ArtistPaintingId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var artistPainting = _context.ArtistPaintings.SingleOrDefault(artistPainting => artistPainting.Id == 1);
            artistPainting.Should().NotBeNull();
        }
    }
}