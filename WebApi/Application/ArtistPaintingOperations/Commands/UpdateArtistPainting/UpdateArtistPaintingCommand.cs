using WebApi.DBOperations;

namespace WebApi.Application.ArtistPaintingOperations.Commands.UpdateArtistPainting
{
    public class UpdateArtistPaintingCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int DataId { get; set; }
        public UpdateArtistPaintingModel Model { get; set; }

        public UpdateArtistPaintingCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var artistPainting = _context.ArtistPaintings.SingleOrDefault(x=> x.Id == DataId);
            if(artistPainting == null)
                throw new InvalidOperationException("Data could not be found!");

            var artist = _context.Artists.SingleOrDefault(x => x.Id == Model.ArtistId);
            if(artist == null)
                throw new InvalidOperationException("Artist could not be found!");

            var painting = _context.Paintings.SingleOrDefault(x => x.Id == Model.PaintingId);
            if(painting == null)
                throw new InvalidOperationException("Painting could not be found!");

            artistPainting.PaintingId = Model.PaintingId != default ? Model.PaintingId : artistPainting.PaintingId;
            artistPainting.ArtistId = Model.ArtistId != default ? Model.ArtistId : artistPainting.ArtistId;
            artistPainting.IsActive = Model.IsActive;

            _context.ArtistPaintings.Update(artistPainting);
            _context.SaveChanges();
        }

    }

    public class UpdateArtistPaintingModel
    {
        public int PaintingId { get; set; }
        public int ArtistId { get; set; }
        public bool IsActive { get; set; }
    }
}