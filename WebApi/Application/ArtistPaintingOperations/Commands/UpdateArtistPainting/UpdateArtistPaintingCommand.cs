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

            var artist = _context.Artists.SingleOrDefault(x => x.Id == Model.ActorId);
            if(artist == null)
                throw new InvalidOperationException("Artist could not be found!");

            var painting = _context.Paintings.SingleOrDefault(x => x.Id == Model.FilmId);
            if(painting == null)
                throw new InvalidOperationException("Painting could not be found!");

            artistPainting.PaintingId = Model.FilmId != default ? Model.FilmId : artistPainting.PaintingId;
            artistPainting.ArtistId = Model.ActorId != default ? Model.ActorId : artistPainting.ArtistId;
            artistPainting.IsActive = Model.IsActive;

            _context.ArtistPaintings.Update(artistPainting);
            _context.SaveChanges();
        }

    }

    public class UpdateArtistPaintingModel
    {
        public int FilmId { get; set; }
        public int ActorId { get; set; }
        public bool IsActive { get; set; }
    }
}