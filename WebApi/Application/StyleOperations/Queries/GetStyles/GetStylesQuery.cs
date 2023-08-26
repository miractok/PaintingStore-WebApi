using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.StyleOperations.Queries.GetStyles
{
    public class GetStylesQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetStylesQuery(IMapper mapper, IPaintingStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<StylesViewModel> Handle()
        {
            var stylesList = _context.Styles.Where(x => x.IsActive).OrderBy(x => x.Id).ToList<Style>();
            List<StylesViewModel> vm = _mapper.Map<List<StylesViewModel>>(stylesList);

            return vm;
        }
    }

    public class StylesViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}