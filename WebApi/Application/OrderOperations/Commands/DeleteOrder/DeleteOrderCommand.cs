using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int DataId { get; set; }

        public DeleteOrderCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var order = _context.Orders.SingleOrDefault(x=> x.Id == DataId);
            if(order == null)
                throw new InvalidOperationException("Data could not be found!");

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}