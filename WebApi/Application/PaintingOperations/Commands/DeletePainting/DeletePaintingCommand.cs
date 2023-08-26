using WebApi.DBOperations;

namespace WebApi.Application.PaintingOperations.Commands.DeletePainting
{
    public class DeletePaintingCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int PaintingId { get; set; }

        public DeletePaintingCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var painting = _context.Paintings.SingleOrDefault(x=> x.Id == PaintingId);
            if(painting == null)
                throw new InvalidOperationException("Painting could not be found!");

            _context.Paintings.Remove(painting);
            _context.SaveChanges();
        }
    }
}