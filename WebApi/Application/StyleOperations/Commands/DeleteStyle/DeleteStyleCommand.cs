using WebApi.DBOperations;

namespace WebApi.Application.StyleOperations.Commands.DeleteStyle
{
    public class DeleteStyleCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int StyleId { get; set; }

        public DeleteStyleCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var style = _context.Styles.SingleOrDefault(x=> x.Id == StyleId);
            if(style == null)
                throw new InvalidOperationException("Style does not exist!");

            var styleCheck = _context.Paintings.Where(x=> x.StyleId == style.Id && style.IsActive).Any();
            if(styleCheck)
                throw new InvalidOperationException("Style cannot be deleted! First remove Style from use.");


            _context.Styles.Remove(style);
            _context.SaveChanges();
        }
    }
}