using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ArtistOperations.Queries.GetArtistDetails;
using WebApi.DBOperations;

namespace Applications.ArtistOperations.Queries.GetArtistDetails
{
    public class GetArtistDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly PaintingStoreDbContext _context;
        readonly IMapper _mapper;

        public GetArtistDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongArtistIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetArtistDetailsQuery query = new GetArtistDetailsQuery(_context, _mapper);
            query.ArtistId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any artist.");
        }

        [Fact]
        public void WhenValidArtistIdIsGiven_Artist_ShouldReturn()
        {
            GetArtistDetailsQuery query = new GetArtistDetailsQuery(_context, _mapper);
            query.ArtistId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var artist = _context.Artists.SingleOrDefault(artist => artist.Id == 1);
            artist.Should().NotBeNull();
        }
    }
}