using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.ArtistOperations.Queries.GetArtistDetails
{
    public class GetArtistDetailsQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public int ArtistId { get; set; }
        public GetArtistDetailsQuery(IPaintingStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ArtistViewIdModel Handle()
        {
            var Artist = _context.Artists.Include(x=> x.ArtistPainting).ThenInclude(x=> x.Painting).Where(Artist=> Artist.Id == ArtistId).SingleOrDefault();

            if(Artist == null)
                throw new InvalidOperationException("The Id you entered does not match any Artist.");

            ArtistViewIdModel vm = _mapper.Map<ArtistViewIdModel>(Artist); 
            
            return vm;
        }
    }

    public class ArtistViewIdModel
    {
        public string NameSurname { get; set; }
        public string Country { get; set; }
        public virtual ICollection<string> Paintings { get; set; }
        public bool IsActive { get; set; } = true;
    }
}