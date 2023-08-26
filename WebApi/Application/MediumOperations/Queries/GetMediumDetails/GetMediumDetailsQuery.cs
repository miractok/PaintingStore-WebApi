using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.MediumOperations.Queries.GetMediumDetails
{
    public class GetMediumDetailsQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public int MediumId { get; set; }
        public GetMediumDetailsQuery(IPaintingStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public MediumViewIdModel Handle()
        {
            var medium = _context.Mediums.Where(x => x.IsActive).Where(medium=> medium.Id == MediumId).SingleOrDefault();

            if(medium== null)
                throw new InvalidOperationException("The Id you entered does not match any Medium.");

            MediumViewIdModel vm = _mapper.Map<MediumViewIdModel>(medium); 
            
            return vm;
        }
    }

    public class MediumViewIdModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}