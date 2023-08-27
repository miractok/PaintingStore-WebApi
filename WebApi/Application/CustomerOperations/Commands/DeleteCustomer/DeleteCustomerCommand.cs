using WebApi.DBOperations;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        private readonly IPaintingStoreDbContext _context;

        public int CustomerId { get; set; }

        public DeleteCustomerCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x=> x.Id == CustomerId);
            if(customer == null)
                throw new InvalidOperationException("Customer could not be found!");

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}