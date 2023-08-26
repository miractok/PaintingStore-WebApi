using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int GenreId { get; set; }

        public DeleteGenreCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=> x.Id == GenreId);
            if(genre == null)
                throw new InvalidOperationException("Genre does not exist!");

            var genreCheck = _context.Paintings.Where(x=> x.GenreId == genre.Id && genre.IsActive).Any();
            if(genreCheck)
                throw new InvalidOperationException("Genre cannot be deleted! First remove genre from use.");


            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}