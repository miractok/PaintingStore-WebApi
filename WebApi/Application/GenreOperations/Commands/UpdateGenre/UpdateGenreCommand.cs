using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }

        public UpdateGenreCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=> x.Id == GenreId);
            if(genre == null)
                throw new InvalidOperationException("Genre does not exist!");

            if(_context.Genres.Any(x=> x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Genre already exists!");

            genre.Name = Model.Name != default ? Model.Name : genre.Name;
            genre.IsActive = Model.IsActive;

            _context.Genres.Update(genre);
            _context.SaveChanges();
        }

    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}