using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.PaintingOperations.Commands.CreatePainting
{
    public class CreatePaintingCommand
    {
        public CreatePaintingModel? Model { get; set; }
        public IPaintingStoreDbContext _context { get; set; }
        public IMapper _mapper { get; set; }

        public CreatePaintingCommand(IMapper mapper, IPaintingStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var painting = _context.Paintings.SingleOrDefault(x=> x.Name.ToLower().Trim() == Model.Name.ToLower().Trim());
            if(painting != null)
                throw new InvalidOperationException("Painting already exists!");
            painting = _mapper.Map<Painting>(Model);
  
            _context.Paintings.Add(painting);
            _context.SaveChanges();
        }
    }

    public class CreatePaintingModel
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