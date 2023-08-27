using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class PaintingStoreDbContext : DbContext, IPaintingStoreDbContext
    {
        public PaintingStoreDbContext(DbContextOptions<PaintingStoreDbContext> options) : base(options)
        { }

        public DbSet<Painting> Paintings { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Medium> Mediums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ArtistPainting> ArtistPaintings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}