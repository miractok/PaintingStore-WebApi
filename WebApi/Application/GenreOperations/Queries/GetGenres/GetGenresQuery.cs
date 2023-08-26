using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(IMapper mapper, IPaintingStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<GenresViewModel> Handle()
        {
            var genresList = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id).ToList<Genre>();
            List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genresList);

            return vm;
        }
    }

    public class GenresViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}