using WebApi.DBOperations;

namespace WebApi.Application.ArtistPaintingOperations.Commands.DeleteArtistPainting
{
    public class DeleteArtistPaintingCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int DataId { get; set; }

        public DeleteArtistPaintingCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var artistPainting = _context.ArtistPaintings.SingleOrDefault(x=> x.Id == DataId);
            if(artistPainting == null)
                throw new InvalidOperationException("Data could not be found!");

            _context.ArtistPaintings.Remove(artistPainting);
            _context.SaveChanges();
        }
    }
}