using WebApi.DBOperations;

namespace WebApi.Application.ArtistOperations.Commands.UpdateArtist
{
    public class UpdateArtistCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int ArtistId { get; set; }
        public UpdateArtistModel Model { get; set; }

        public UpdateArtistCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var artist = _context.Artists.SingleOrDefault(x=> x.Id == ArtistId);
            if(artist == null)
                throw new InvalidOperationException("Artist could not be found!");

            if(_context.Artists.Any(x=> x.NameSurname.ToLower() == Model.NameSurname.ToLower() && x.Id != ArtistId))
                throw new InvalidOperationException("Artist already exists!");

            artist.NameSurname = Model.NameSurname != default ? Model.NameSurname : artist.NameSurname;
            artist.Country = Model.Country != default ? Model.Country : artist.Country; 

            _context.Artists.Update(artist);
            _context.SaveChanges();
        }

    }

    public class UpdateArtistModel
    {
        public string NameSurname { get; set; }
        public string Country { get; set; }
    }
}