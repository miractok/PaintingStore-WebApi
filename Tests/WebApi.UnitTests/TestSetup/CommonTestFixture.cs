using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup
{
    public class CommonTestFixture
    {
        public PaintingStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<PaintingStoreDbContext>().UseInMemoryDatabase(databaseName:"PaintingStoreTestDB").Options;
            Context = new PaintingStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddData();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>();}).CreateMapper();
        }
    }
}