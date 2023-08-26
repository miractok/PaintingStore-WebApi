using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.PaintingOperations.Queries.GetPaintings
{
    public class GetPaintingsQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetPaintingsQuery(IPaintingStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<PaintingsViewModel> Handle()
        {
            var paintingList = _context.Paintings.Include(x=> x.Artist).Include(x=> x.Genre).Include(x=> x.Style).Include(x => x.Medium).OrderBy(x=> x.Id).ToList<Painting>();
            List<PaintingsViewModel> vm = _mapper.Map<List<PaintingsViewModel>>(paintingList);

            return vm;
        }
    }

    public class PaintingsViewModel
    {
        public string? Name { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Artist { get; set; }
        public string? Style { get; set; }
        public string? Genre { get; set; }
        public string? Subject { get; set; }
        public string? Medium { get; set; }
        public int Price { get; set; }
    }
}