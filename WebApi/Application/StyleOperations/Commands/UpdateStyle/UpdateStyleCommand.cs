using WebApi.DBOperations;

namespace WebApi.Application.StyleOperations.Commands.UpdateStyle
{
    public class UpdateStyleCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int StyleId { get; set; }
        public UpdateStyleModel Model { get; set; }

        public UpdateStyleCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var style = _context.Styles.SingleOrDefault(x=> x.Id == StyleId);
            if(style == null)
                throw new InvalidOperationException("Style does not exist!");

            if(_context.Styles.Any(x=> x.Name.ToLower() == Model.Name.ToLower() && x.Id != StyleId))
                throw new InvalidOperationException("Style already exists!");

            style.Name = Model.Name != default ? Model.Name : style.Name;
            style.IsActive = Model.IsActive;

            _context.Styles.Update(style);
            _context.SaveChanges();
        }

    }

    public class UpdateStyleModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}