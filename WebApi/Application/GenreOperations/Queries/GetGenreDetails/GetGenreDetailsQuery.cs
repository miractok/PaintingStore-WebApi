using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailsQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public int GenreId { get; set; }
        public GetGenreDetailsQuery(IPaintingStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenresViewIdModel Handle()
        {
            var genre = _context.Genres.Where(x => x.IsActive).Where(genre=> genre.Id == GenreId).SingleOrDefault();

            if(genre== null)
                throw new InvalidOperationException("The Id you entered does not match any genre.");

            GenresViewIdModel vm = _mapper.Map<GenresViewIdModel>(genre); 
            
            return vm;
        }
    }

    public class GenresViewIdModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}