using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ArtistPaintingOperations.Commands.CreateArtistPainting
{
    public class CreateArtistPaintingCommand
    {
        public CreateArtistPaintingViewModel Model { get; set; }
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateArtistPaintingCommand(IMapper mapper, IPaintingStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var artistPainting = _context.ArtistPaintings.SingleOrDefault(x => x.ArtistId == Model.ArtistId && x.PaintingId == Model.PaintingId);
            if(artistPainting != null)
                throw new InvalidOperationException("This relation already exists.");

            var artist = _context.Artists.SingleOrDefault(x => x.Id == Model.ArtistId);
            if(artist == null)
                throw new InvalidOperationException("Artist could not be found!");

            var painting = _context.Paintings.SingleOrDefault(x => x.Id == Model.PaintingId);
            if(painting == null)
                throw new InvalidOperationException("Painting could not be found!");

            artistPainting = _mapper.Map<ArtistPainting>(Model);

            _context.ArtistPaintings.Add(artistPainting);
            _context.SaveChanges();
        }
    }

    public class CreateArtistPaintingViewModel
    {
        public int PaintingId { get; set; }
        public int ArtistId { get; set; }
    }
}