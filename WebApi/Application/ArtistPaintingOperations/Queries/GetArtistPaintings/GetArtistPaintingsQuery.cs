using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ArtistPaintingOperations.Queries.GetArtistPaintings
{
    public class GetArtistPaintingQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetArtistPaintingQuery(IMapper mapper, IPaintingStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<ArtistPaintingViewModel> Handle()
        {
            var artistPaintingList = _context.ArtistPaintings.Include(x => x.Painting).Include(x => x.Artist).OrderBy(x=> x.Id).ToList<ArtistPainting>();
            List<ArtistPaintingViewModel> vm = _mapper.Map<List<ArtistPaintingViewModel>>(artistPaintingList);

            return vm;
        }

    }

    public class ArtistPaintingViewModel
    {
        public int PaintingId { get; set; }
        public int ArtistId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}