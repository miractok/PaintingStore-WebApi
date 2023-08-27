using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int DataId { get; set; }
        public UpdateOrderModel Model { get; set; }

        public UpdateOrderCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var order = _context.Orders.SingleOrDefault(x=> x.Id == DataId);
            if(order == null)
                throw new InvalidOperationException("Order could not be found!");

            var customer = _context.Customers.SingleOrDefault(x => x.Id == Model.CustomerId);
            if(customer == null)
                throw new InvalidOperationException("Customer could not be found!");

            var film = _context.Paintings.SingleOrDefault(x => x.Id == Model.PaintingId);
            if(film == null)
                throw new InvalidOperationException("Film could not be found!");

            order.PaintingId = Model.PaintingId != default ? Model.PaintingId : order.PaintingId;
            order.CustomerId = Model.CustomerId != default ? Model.CustomerId : order.CustomerId;
            order.IsActive = Model.IsActive;

            _context.Orders.Update(order);
            _context.SaveChanges();
        }

    }

    public class UpdateOrderModel
    {
        public int CustomerId { get; set; }
        public int PaintingId { get; set; }
        public bool IsActive { get; set; }
    }
}