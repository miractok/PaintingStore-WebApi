using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.MediumOperations.Queries.GetMediumDetails;
using WebApi.DBOperations;

namespace Applications.MediumOperations.Queries.GetMediumDetails
{
    public class GetMediumDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly PaintingStoreDbContext _context;
        readonly IMapper _mapper;

        public GetMediumDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongMediumIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            GetMediumDetailsQuery query = new GetMediumDetailsQuery(_context, _mapper);
            query.MediumId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("The Id you entered does not match any Medium.");
        }

        [Fact]
        public void WhenValidMediumIdIsGiven_Medium_ShouldReturn()
        {
            GetMediumDetailsQuery query = new GetMediumDetailsQuery(_context, _mapper);
            query.MediumId = 3;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var Medium = _context.Mediums.SingleOrDefault(Medium => Medium.Id == query.MediumId);
            Medium.Should().NotBeNull();
        }
    }
}