using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.DBOperations;

namespace Applications.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly PaintingStoreDbContext _context;
        readonly IMapper _mapper;

        public GetGenreDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongGenreIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetGenreDetailsQuery query = new GetGenreDetailsQuery(_context, _mapper);
            query.GenreId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any genre.");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldReturn()
        {
            GetGenreDetailsQuery query = new GetGenreDetailsQuery(_context, _mapper);
            query.GenreId = 3;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == query.GenreId);
            genre.Should().NotBeNull();
        }
    }
}