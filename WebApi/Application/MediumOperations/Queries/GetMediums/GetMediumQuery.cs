using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MediumOperations.Queries.GetMediums
{
    public class GetMediumsQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMediumsQuery(IMapper mapper, IPaintingStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<MediumsViewModel> Handle()
        {
            var mediumsList = _context.Mediums.Where(x => x.IsActive).OrderBy(x => x.Id).ToList<Medium>();
            List<MediumsViewModel> vm = _mapper.Map<List<MediumsViewModel>>(mediumsList);

            return vm;
        }
    }

    public class MediumsViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}