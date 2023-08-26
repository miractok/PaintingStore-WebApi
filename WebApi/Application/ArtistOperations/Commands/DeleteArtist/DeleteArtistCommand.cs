using WebApi.DBOperations;

namespace WebApi.Application.ArtistOperations.Commands.DeleteArtist
{
    public class DeleteArtistCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int ArtistId { get; set; }

        public DeleteArtistCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var artist = _context.Artists.SingleOrDefault(x=> x.Id == ArtistId);
            if(artist == null)
                throw new InvalidOperationException("Artist could not be found!");

            var artistCheck = _context.ArtistPaintings.Where(x=> x.ArtistId == artist.Id).Any();
            if(artistCheck)
                throw new InvalidOperationException("Artist cannot be deleted! First remove Artist from the painting.");

            _context.Artists.Remove(artist);
            _context.SaveChanges();
        }
    }
}