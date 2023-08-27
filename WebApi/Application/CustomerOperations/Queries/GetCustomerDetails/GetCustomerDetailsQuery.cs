using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerDetails
{
    public class GetCustomerDetailsQuery
    {
        private readonly IPaintingStoreDbContext _context;
        private readonly IMapper _mapper;
        public int CustomerId { get; set; }
        public GetCustomerDetailsQuery(IPaintingStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CustomerViewIdModel Handle()
        {
            var customer = _context.Customers.Include(x => x.Orders).ThenInclude(x => x.Painting).Where(customer => customer.Id == CustomerId).SingleOrDefault();

            if(customer == null)
                throw new InvalidOperationException("The Id you entered does not match any customer.");

            CustomerViewIdModel vm = _mapper.Map<CustomerViewIdModel>(customer); 
            
            return vm;
        }
    }

    public class CustomerViewIdModel
    {
        public string NameSurname { get; set; }
        public IReadOnlyCollection<string> Orders { get; set; }
        public bool IsActive { get; set; } = true;
    }
}