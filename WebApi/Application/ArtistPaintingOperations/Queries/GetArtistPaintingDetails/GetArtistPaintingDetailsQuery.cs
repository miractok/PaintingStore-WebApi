using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.ArtistPaintingOperations.Queries.GetArtistPaintingDetails
{
    public class GetArtistPaintingDetailsQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public int ArtistPaintingId { get; set; }
        public GetArtistPaintingDetailsQuery(IPaintingStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ArtistPaintingViewIdModel Handle()
        {
            var artistPainting = _context.ArtistPaintings.Include(x=> x.Artist).Include(x=> x.Painting).Where(artistPainting=> artistPainting.Id == ArtistPaintingId).SingleOrDefault();

            if(artistPainting == null)
                throw new InvalidOperationException("The Id you entered does not match any ArtistPainting relation.");

            ArtistPaintingViewIdModel vm = _mapper.Map<ArtistPaintingViewIdModel>(artistPainting); 
            
            return vm;
        }
    }

    public class ArtistPaintingViewIdModel
    {
        public int PaintingId { get; set; }
        public int ArtistId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}