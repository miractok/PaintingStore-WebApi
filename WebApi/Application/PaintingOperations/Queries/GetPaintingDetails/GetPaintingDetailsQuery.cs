using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.PaintingOperations.Queries.GetPaintingDetails
{
    public class GetPaintingDetailsQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public int PaintingId { get; set; }
        public GetPaintingDetailsQuery(IPaintingStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PaintingViewIdModel Handle()
        {
            var Painting = _context.Paintings.Include(x=> x.Artist).Include(x=> x.Genre).Include(x=> x.Style).Include(x => x.Medium).Where(Painting=> Painting.Id == PaintingId).SingleOrDefault();
            if(Painting == null)
                throw new InvalidOperationException("The Id you entered does not match any painting.");
            PaintingViewIdModel vm = _mapper.Map<PaintingViewIdModel>(Painting); 
            return vm;
        }
    }

    public class PaintingViewIdModel
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