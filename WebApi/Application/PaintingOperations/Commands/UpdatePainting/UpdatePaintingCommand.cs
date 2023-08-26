using WebApi.DBOperations;

namespace WebApi.Application.PaintingOperations.Commands.UpdatePainting
{
    public class UpdatePaintingCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int PaintingId { get; set; }
        public UpdatePaintingModel Model { get; set; }

        public UpdatePaintingCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var painting = _context.Paintings.SingleOrDefault(x=> x.Id == PaintingId);
            if(painting == null)
                throw new InvalidOperationException("Painting could not be found!");

            if(_context.Paintings.Any(x=> x.Name.ToLower() == Model.Name.ToLower() && x.Id != PaintingId))
                throw new InvalidOperationException("Painting already exists!");

            painting.Name = Model.Name != default ? Model.Name : painting.Name;
            painting.PublishDate = Model.PublishDate != default ? Model.PublishDate : painting.PublishDate;
            painting.ArtistId = Model.ArtistId != default ? Model.ArtistId : painting.ArtistId;
            painting.StyleId = Model.StyleId != default ? Model.StyleId : painting.StyleId;
            painting.GenreId = Model.GenreId != default ? Model.GenreId : painting.GenreId;
            painting.MediumId = Model.MediumId != default ? Model.MediumId : painting.MediumId;
            painting.Subject = Model.Subject != default ? Model.Subject : painting.Subject;
            painting.Price = Model.Price != default ? Model.Price : painting.Price;

            _context.Paintings.Update(painting);
            _context.SaveChanges();
        }

    }

    public class UpdatePaintingModel
    {
        public string? Name { get; set; }
        public DateTime PublishDate { get; set; }
        public int ArtistId { get; set; }
        public int StyleId { get; set; }
        public int GenreId { get; set; }
        public int MediumId { get; set; }
        public string? Subject { get; set; }
        public int Price { get; set; }
    }
}