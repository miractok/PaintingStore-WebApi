using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ArtistOperations.Commands.CreateArtist
{
    public class CreateArtistCommand
    {
        public CreateArtistViewModel Model { get; set; }
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateArtistCommand(IMapper mapper, IPaintingStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var artist = _context.Artists.SingleOrDefault(x => x.NameSurname == Model.NameSurname);
            if(artist != null)
                throw new InvalidOperationException("This artist already exists.");

            artist = _mapper.Map<Artist>(Model);

            _context.Artists.Add(artist);
            _context.SaveChanges();
        }
    }

    public class CreateArtistViewModel
    {
        public string NameSurname { get; set; }
        public string Country { get; set; }
    }
}