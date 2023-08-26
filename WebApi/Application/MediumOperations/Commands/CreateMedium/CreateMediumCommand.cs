using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MediumOperations.Commands.CreateMedium
{
    public class CreateMediumCommand
    {
        public CreateMediumViewModel Model { get; set; }
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMediumCommand(IMapper mapper, IPaintingStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var medium = _context.Mediums.SingleOrDefault(x => x.Name == Model.Name);
            if(medium != null)
                throw new InvalidOperationException("This medium already exists.");

            medium = _mapper.Map<Medium>(Model);

            _context.Mediums.Add(medium);
            _context.SaveChanges();
        }
    }

    public class CreateMediumViewModel
    {
        public string Name { get; set; }
    }
}