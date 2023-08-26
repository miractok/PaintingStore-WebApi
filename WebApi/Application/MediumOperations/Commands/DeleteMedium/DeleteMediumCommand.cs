using WebApi.DBOperations;

namespace WebApi.Application.MediumOperations.Commands.DeleteMedium
{
    public class DeleteMediumCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int MediumId { get; set; }

        public DeleteMediumCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var medium = _context.Mediums.SingleOrDefault(x=> x.Id == MediumId);
            if(medium == null)
                throw new InvalidOperationException("Medium does not exist!");

            var mediumCheck = _context.Paintings.Where(x=> x.MediumId == medium.Id && medium.IsActive).Any();
            if(mediumCheck)
                throw new InvalidOperationException("Medium cannot be deleted! First remove medium from use.");


            _context.Mediums.Remove(medium);
            _context.SaveChanges();
        }
    }
}