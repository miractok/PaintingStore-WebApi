using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.StyleOperations.Commands.CreateStyle
{
    public class CreateStyleCommand
    {
        public CreateStyleViewModel Model { get; set; }
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateStyleCommand(IMapper mapper, IPaintingStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var style = _context.Styles.SingleOrDefault(x => x.Name == Model.Name);
            if(style != null)
                throw new InvalidOperationException("This Style already exists.");

            style = _mapper.Map<Style>(Model);

            _context.Styles.Add(style);
            _context.SaveChanges();
        }
    }

    public class CreateStyleViewModel
    {
        public string Name { get; set; }
    }
}