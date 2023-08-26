using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class ArtistPainting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }
        public int PaintingId { get; set; }
        public Painting? Painting { get; set; }
        public bool IsActive { get; set; } = true;

    }
}