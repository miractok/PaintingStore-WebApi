using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.StyleOperations.Queries.GetStyleDetails
{
    public class GetStyleDetailsQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public int StyleId { get; set; }
        public GetStyleDetailsQuery(IPaintingStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public StyleViewIdModel Handle()
        {
            var Style = _context.Styles.Where(x => x.IsActive).Where(Style=> Style.Id == StyleId).SingleOrDefault();

            if(Style== null)
                throw new InvalidOperationException("The Id you entered does not match any Style.");

            StyleViewIdModel vm = _mapper.Map<StyleViewIdModel>(Style); 
            
            return vm;
        }
    }

    public class StyleViewIdModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}