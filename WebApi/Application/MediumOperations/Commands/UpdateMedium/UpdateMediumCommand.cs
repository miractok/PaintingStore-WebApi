using WebApi.DBOperations;

namespace WebApi.Application.MediumOperations.Commands.UpdateMedium
{
    public class UpdateMediumCommand
    {
        private readonly IPaintingStoreDbContext _context;
        public int MediumId { get; set; }
        public UpdateMediumModel Model { get; set; }

        public UpdateMediumCommand(IPaintingStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var medium = _context.Mediums.SingleOrDefault(x=> x.Id == MediumId);
            if(medium == null)
                throw new InvalidOperationException("Medium does not exist!");

            if(_context.Mediums.Any(x=> x.Name.ToLower() == Model.Name.ToLower() && x.Id != MediumId))
                throw new InvalidOperationException("Medium already exists!");

            medium.Name = Model.Name != default ? Model.Name : medium.Name;
            medium.IsActive = Model.IsActive;

            _context.Mediums.Update(medium);
            _context.SaveChanges();
        }

    }

    public class UpdateMediumModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}