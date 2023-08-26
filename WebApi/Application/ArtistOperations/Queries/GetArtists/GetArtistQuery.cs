using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ArtistOperations.Queries.GetArtists
{
    public class GetArtistsQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetArtistsQuery(IMapper mapper, IPaintingStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<ArtistsViewModel> Handle()
        {
            var artistList = _context.Artists.Include(x=> x.ArtistPainting).ThenInclude(x=> x.Painting).OrderBy(x=> x.Id).ToList<Artist>();
            List<ArtistsViewModel> vm = _mapper.Map<List<ArtistsViewModel>>(artistList);

            return vm;
        }

    }

    public class ArtistsViewModel
    {
        public string NameSurname { get; set; }
        public string Country { get; set; }
        public virtual ICollection<string> Paintings { get; set; }
        public bool IsActive { get; set; } = true;
    }
}